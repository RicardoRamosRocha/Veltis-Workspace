using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Domain.Constants;
using Veltis.Workspace.Web.Models.Admin;

namespace Veltis.Workspace.Web.Controllers.Admin;

[Authorize(Roles = ApplicationRoles.AdminAccess)]
[Route("admin")]
public sealed class AdminController : Controller
{
    private readonly IApplicationDbContext _context;

    public AdminController(IApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        AdminDashboardViewModel model = new()
        {
            Professions = await _context.Professions.CountAsync(),
            Agents = await _context.Agents.CountAsync(),
            Forms = await _context.FormDefinitions.CountAsync(),
            Prompts = await _context.PromptTemplates.CountAsync(),
            Providers = await _context.AIProviders.CountAsync(),
            Models = await _context.AIModels.CountAsync(),
            Tenants = await _context.Tenants.CountAsync(),
            Plans = await _context.Plans.CountAsync(),
            Users = await _context.Users.CountAsync()
        };

        return View(model);
    }

    [HttpGet("audit")]
    public async Task<IActionResult> Audit(string? userId, string? action, string? entityName, DateTime? date)
    {
        var query = _context.AuditLogs.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(userId) && Guid.TryParse(userId, out Guid parsedUserId))
        {
            query = query.Where(log => log.UserId == parsedUserId);
        }

        if (!string.IsNullOrWhiteSpace(action))
        {
            query = query.Where(log => log.Action.Contains(action));
        }

        if (!string.IsNullOrWhiteSpace(entityName))
        {
            query = query.Where(log => log.EntityName.Contains(entityName));
        }

        if (date.HasValue)
        {
            DateTime start = DateTime.SpecifyKind(date.Value.Date, DateTimeKind.Utc);
            DateTime end = start.AddDays(1);
            query = query.Where(log => log.OccurredAt >= start && log.OccurredAt < end);
        }

        AdminAuditListViewModel model = new()
        {
            UserId = userId,
            Action = action,
            EntityName = entityName,
            Date = date,
            Rows = await query
                .OrderByDescending(log => log.OccurredAt)
                .Take(100)
                .Select(log => new AdminAuditRowViewModel
                {
                    UserId = log.UserId,
                    Action = log.Action,
                    EntityName = log.EntityName,
                    EntityId = log.EntityId,
                    OccurredAt = log.OccurredAt
                })
                .ToListAsync()
        };

        return View(model);
    }
}
