using Microsoft.AspNetCore.Authorization;

namespace Web.Api.Extensions;

public static class AuthorizationExtensions
{
    public static TBuilder RequireRole<TBuilder>(this TBuilder builder, string role)
        where TBuilder : IEndpointConventionBuilder
    {
        return builder.RequireAuthorization(new AuthorizeAttribute { Roles = role });
    }
}
