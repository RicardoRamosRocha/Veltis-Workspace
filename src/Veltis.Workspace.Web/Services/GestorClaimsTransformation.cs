using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Domain.Constants;

namespace Veltis.Workspace.Web.Services;

public sealed class GestorClaimsTransformation : IClaimsTransformation
{
    private readonly IApplicationDbContext _context;

    public GestorClaimsTransformation(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.Identity?.IsAuthenticated != true ||
            principal.IsInRole(ApplicationRoles.Administrator) ||
            principal.IsInRole(ApplicationRoles.Gestor))
        {
            return principal;
        }

        string? userIdValue = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdValue, out Guid userId))
        {
            return principal;
        }

        bool isGestor = await _context.Users
            .Include(user => user.Profession)
            .AnyAsync(user =>
                user.Id == userId &&
                user.IsActive &&
                user.Profession != null &&
                (user.Profession.Name == ApplicationRoles.Gestor || user.Profession.Slug == ApplicationRoles.Gestor));

        if (!isGestor)
        {
            return principal;
        }

        ClaimsIdentity identity = new(principal.Identity);
        identity.AddClaim(new Claim(ClaimTypes.Role, ApplicationRoles.Gestor));
        principal.AddIdentity(identity);

        return principal;
    }
}
