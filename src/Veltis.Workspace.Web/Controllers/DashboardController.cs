using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Application.Dashboard;
using Veltis.Workspace.Web.Models.Dashboard;

namespace Veltis.Workspace.Web.Controllers;

[Authorize]
[Route("dashboard")]
public sealed class DashboardController : Controller
{
    private readonly IDashboardService _dashboardService;
    private readonly ICurrentUserService _currentUserService;

    public DashboardController(
        IDashboardService dashboardService,
        ICurrentUserService currentUserService)
    {
        _dashboardService = dashboardService;
        _currentUserService = currentUserService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        if (_currentUserService.UserId is not Guid userId)
        {
            return Challenge();
        }

        DashboardDto? dashboard = await _dashboardService.GetForUserAsync(userId);
        if (dashboard is null)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(new DashboardViewModel
        {
            UserName = dashboard.UserName,
            ProfessionName = dashboard.ProfessionName,
            WorkspaceName = dashboard.WorkspaceName,
            DocumentsCount = dashboard.DocumentsCount,
            AgentsCount = dashboard.AgentsCount,
            TemplatesCount = dashboard.TemplatesCount,
            HistoryCount = dashboard.HistoryCount
        });
    }
}
