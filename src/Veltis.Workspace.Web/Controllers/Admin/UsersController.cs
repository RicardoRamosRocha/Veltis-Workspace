using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Application.Professions;
using Veltis.Workspace.Application.Workspaces;
using Veltis.Workspace.Domain.Constants;
using Veltis.Workspace.Domain.Entities;
using Veltis.Workspace.Domain.Identity;
using Veltis.Workspace.Web.Models.Admin;

namespace Veltis.Workspace.Web.Controllers.Admin;

[Authorize(Roles = ApplicationRoles.Administrator)]
[Route("admin/users")]
public sealed class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _context;
    private readonly IProfessionService _professionService;
    private readonly IWorkspaceService _workspaceService;

    public UsersController(
        UserManager<ApplicationUser> userManager,
        IApplicationDbContext context,
        IProfessionService professionService,
        IWorkspaceService workspaceService)
    {
        _userManager = userManager;
        _context = context;
        _professionService = professionService;
        _workspaceService = workspaceService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        List<UserAdminViewModel> users = await _context.Users
            .Select(user => new UserAdminViewModel
            {
                Id = user.Id,
                DisplayName = user.DisplayName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                ProfessionName = user.Profession == null ? "Nao definida" : user.Profession.Name,
                IsActive = user.IsActive
            })
            .OrderBy(user => user.DisplayName)
            .ToListAsync();

        return View(users);
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        await PopulateProfessionsAsync();
        return View(new UserCreateViewModel());
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateProfessionsAsync();
            return View(model);
        }

        if (model.ProfessionId is null)
        {
            ModelState.AddModelError(nameof(model.ProfessionId), "Selecione uma profissao valida.");
            await PopulateProfessionsAsync();
            return View(model);
        }

        var user = new ApplicationUser
        {
            DisplayName = model.DisplayName,
            Email = model.Email,
            UserName = model.Email,
            ProfessionId = model.ProfessionId,
            IsActive = model.IsActive
        };

        IdentityResult result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            await PopulateProfessionsAsync();
            return View(model);
        }

        _context.UserProfessions.Add(new UserProfession
        {
            UserId = user.Id,
            ProfessionId = model.ProfessionId.Value,
            IsPrimary = true
        });
        await _context.SaveChangesAsync();
        await _workspaceService.EnsureForUserAsync(user.Id, user.DisplayName ?? user.Email ?? "Usuario");

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        ApplicationUser? user = await _context.Users.FirstOrDefaultAsync(item => item.Id == id);
        if (user is null)
        {
            return NotFound();
        }

        await PopulateProfessionsAsync();
        return View(new UserEditViewModel
        {
            Id = user.Id,
            DisplayName = user.DisplayName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            ProfessionId = user.ProfessionId,
            IsActive = user.IsActive
        });
    }

    [HttpPost("edit/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UserEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateProfessionsAsync();
            return View(model);
        }

        ApplicationUser? user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            return NotFound();
        }

        user.DisplayName = model.DisplayName;
        user.Email = model.Email;
        user.UserName = model.Email;
        user.ProfessionId = model.ProfessionId;
        user.IsActive = model.IsActive;

        IdentityResult result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            await PopulateProfessionsAsync();
            return View(model);
        }

        if (model.ProfessionId is Guid professionId)
        {
            bool linkExists = await _context.UserProfessions.AnyAsync(item =>
                item.UserId == user.Id && item.ProfessionId == professionId);

            if (!linkExists)
            {
                _context.UserProfessions.Add(new UserProfession
                {
                    UserId = user.Id,
                    ProfessionId = professionId,
                    IsPrimary = true
                });
                await _context.SaveChangesAsync();
            }
        }

        await _workspaceService.EnsureForUserAsync(user.Id, user.DisplayName ?? user.Email ?? "Usuario");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("toggle/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(Guid id)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(id.ToString());
        if (user is not null)
        {
            user.IsActive = !user.IsActive;
            await _userManager.UpdateAsync(user);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("delete/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(id.ToString());
        if (user is not null)
        {
            await _userManager.DeleteAsync(user);
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateProfessionsAsync()
    {
        IReadOnlyCollection<ProfessionDto> professions = await _professionService.GetActiveAsync();
        ViewBag.Professions = professions
            .Select(profession => new SelectListItem(profession.Name, profession.Id.ToString()))
            .ToList();
    }
}
