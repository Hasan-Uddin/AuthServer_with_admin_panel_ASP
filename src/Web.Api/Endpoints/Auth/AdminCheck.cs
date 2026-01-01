using Application.Abstractions.Authentication;
using Domain.Roles;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Auth;

internal sealed class AdminCheck : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("auth/admin-check", async (
            IUserContext userContext,
            CancellationToken cancellationToken) =>
        {
            if (!userContext.IsAuthenticated)
            {
                return Results.Unauthorized();
            }
            return Results.NoContent();
        })
        .WithTags(Tags.Users)
        .RequireRole(RoleCode.Admin);
    }
}
