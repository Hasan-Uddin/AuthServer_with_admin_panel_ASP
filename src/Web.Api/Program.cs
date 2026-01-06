using System.Net;
using System.Reflection;
using Application;
using HealthChecks.UI.Client;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using Web.Api;
using Web.Api.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string[] allowedOrigins = builder.Configuration
                                    .GetSection("Cors:AllowedOrigins")
                                    .Get<string[]>() ?? [];

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddSwaggerGenWithAuth();

builder.Services.AddCertificateForwarding(options => options.CertificateHeader = "X-SSL-CERT");

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options => options.AddPolicy("Allowed_Origins", builder => builder
            .WithOrigins(allowedOrigins)
            .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH")
            .WithHeaders("Content-Type", "Authorization")
            .AllowCredentials()));

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

// ----------------- OpenIddict configuration ----------------------
builder.Services.AddOpenIddictConfiguration(builder.Configuration);

// Authentication configuration
builder.Services.AddAuthenticationConfiguration(builder.Configuration);

// ------------------------------------------------------------

WebApplication app = builder.Build();

bool? AlwaysRunSwagger = builder.Configuration.GetValue<bool>("AuthServer:AlwaysRunSwagger");

if (app.Environment.IsDevelopment() || AlwaysRunSwagger == true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// ------------------ proxy (need for SSL)----------------------
string[]? knownProxies = builder.Configuration.GetSection("AuthServer:KnownProxies").Get<string[]>();

var forwardedHeadersOptions = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto
};

if (knownProxies != null)
{
    foreach (string proxy in knownProxies)
    {
        if (IPAddress.TryParse(proxy, out IPAddress? ip))
        {
            forwardedHeadersOptions.KnownProxies.Add(ip);
        }
    }
}
else
{
    forwardedHeadersOptions.KnownProxies.Add(IPAddress.Parse("172.18.240.206")); // remote IP address
}
app.UseForwardedHeaders(forwardedHeadersOptions);
// ------------------------------------------------------------

app.UseCertificateForwarding();

app.UseRequestContextLogging();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler();

app.UseSerilogRequestLogging();

app.UseRouting();

app.UseCors("Allowed_Origins");

app.UseAuthentication();

app.UseAuthorization();

app.MapEndpoints();

app.MapGet("/remote-ip", (HttpContext ctx) =>   // Debug, get remote IP address
    ctx.Connection.RemoteIpAddress?.ToString()
    + "\n\ntest-scheme: "
    + ctx.Request.Scheme);

await app.RunAsync();

// REMARK: Required for functional and integration tests to work.
namespace Web.Api
{
    public partial class Program;
}
