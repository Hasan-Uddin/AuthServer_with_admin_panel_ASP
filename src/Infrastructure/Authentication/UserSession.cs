using System.Security.Claims;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Domain.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Infrastructure.Authentication;

public sealed class UserSession : IUserSession
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IApplicationDbContext _context;

    public UserSession(
        IHttpContextAccessor httpContextAccessor,
        IApplicationDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public async Task SignInAsync(User user, CancellationToken cancellationToken)
    {
        var identity = new ClaimsIdentity(
            "AuthCookie",
            ClaimTypes.Name,
            ClaimTypes.Role);

        identity.AddClaim(OpenIddictConstants.Claims.Subject, user.Id.ToString());
        identity.AddClaim(OpenIddictConstants.Claims.Email, user.Email);
        identity.AddClaim(ClaimTypes.NameIdentifier, user.Id.ToString());

        List<string> roles = await _context.UserRoles
            .AsNoTracking()
            .Where(ur => ur.UserId == user.Id)
            .Join(_context.Roles,
                  ur => ur.RoleId,
                  r => r.Id,
                  (ur, r) => r.RoleCode)
            .ToListAsync(cancellationToken);

        foreach (string role in roles)
        {
            identity.AddClaim(ClaimTypes.Role, role);
            identity.AddClaim(OpenIddictConstants.Claims.Role, role);
        }
        
        var principal = new ClaimsPrincipal(identity);

        await _httpContextAccessor.HttpContext!.SignInAsync("AuthCookie", principal);
    }
}
