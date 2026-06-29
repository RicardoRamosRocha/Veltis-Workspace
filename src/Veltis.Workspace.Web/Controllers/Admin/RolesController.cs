using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Veltis.Workspace.Domain.Constants;
using Veltis.Workspace.Domain.Identity;
using Veltis.Workspace.Web.Models.Admin;

namespace Veltis.Workspace.Web.Controllers.Admin;

[Authorize(Roles = ApplicationRoles.Administrator)]
[Route("admin/roles")]
public sealed class RolesController : Controller
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RolesController(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View(_roleManager.Roles.OrderBy(role => role.Name).ToList());
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View(new RoleFormViewModel());
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RoleFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole
        {
            Name = model.Name,
            Description = model.Description,
            IsSystemRole = model.IsSystemRole
        });

        if (result.Succeeded)
        {
            return RedirectToAction(nameof(Index));
        }

        AddErrors(result);
        return View(model);
    }

    [HttpGet("edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        ApplicationRole? role = await _roleManager.FindByIdAsync(id.ToString());
        if (role is null)
        {
            return NotFound();
        }

        return View(new RoleFormViewModel
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty,
            Description = role.Description,
            IsSystemRole = role.IsSystemRole
        });
    }

    [HttpPost("edit/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, RoleFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        ApplicationRole? role = await _roleManager.FindByIdAsync(id.ToString());
        if (role is null)
        {
            return NotFound();
        }

        role.Name = model.Name;
        role.Description = model.Description;
        role.IsSystemRole = model.IsSystemRole;

        IdentityResult result = await _roleManager.UpdateAsync(role);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(Index));
        }

        AddErrors(result);
        return View(model);
    }

    [HttpPost("delete/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        ApplicationRole? role = await _roleManager.FindByIdAsync(id.ToString());
        if (role is not null && !role.IsSystemRole)
        {
            await _roleManager.DeleteAsync(role);
        }

        return RedirectToAction(nameof(Index));
    }

    private void AddErrors(IdentityResult result)
    {
        foreach (IdentityError error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}
