using System.Security.Claims;
using Application.Abstractions.Messaging;
using Application.Businesses.Create;
using Domain.Businesses;
using OpenIddict.Abstractions;

namespace Web.Api.Endpoints.Auth;

public class Me : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("auth/me", (HttpContext context) =>
        {
            if (!context.User.Identity?.IsAuthenticated ?? true)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(new
            {
                id = context.User.FindFirstValue(OpenIddictConstants.Claims.Subject),
                email = context.User.FindFirstValue(OpenIddictConstants.Claims.Email)
            });
        })
            .RequireAuthorization();
    }
}
