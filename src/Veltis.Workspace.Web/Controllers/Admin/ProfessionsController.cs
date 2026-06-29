using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Veltis.Workspace.Application.Common.Results;
using Veltis.Workspace.Application.Professions;
using Veltis.Workspace.Domain.Constants;
using Veltis.Workspace.Web.Models.Admin;

namespace Veltis.Workspace.Web.Controllers.Admin;

[Authorize(Roles = ApplicationRoles.AdminAccess)]
[Route("admin/professions")]
public sealed class ProfessionsController : Controller
{
    private readonly IProfessionService _professionService;

    public ProfessionsController(IProfessionService professionService)
    {
        _professionService = professionService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        return View(await _professionService.GetAllAsync());
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View(new ProfessionFormViewModel());
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProfessionFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        Result<Guid> result = await _professionService.CreateAsync(ToInput(model));
        if (result.IsSuccess)
        {
            return RedirectToAction(nameof(Index));
        }

        AddErrors(result);
        return View(model);
    }

    [HttpGet("edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        ProfessionDto? profession = await _professionService.GetByIdAsync(id);
        if (profession is null)
        {
            return NotFound();
        }

        return View(new ProfessionFormViewModel
        {
            Id = profession.Id,
            Name = profession.Name,
            Description = profession.Description,
            Icon = profession.Icon,
            Color = profession.Color,
            Slug = profession.Slug,
            Active = profession.Active
        });
    }

    [HttpPost("edit/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ProfessionFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        Result result = await _professionService.UpdateAsync(id, ToInput(model));
        if (result.IsSuccess)
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
        await _professionService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private static ProfessionInputDto ToInput(ProfessionFormViewModel model)
    {
        return new ProfessionInputDto
        {
            Name = model.Name,
            Description = model.Description,
            Icon = model.Icon,
            Color = model.Color,
            Slug = model.Slug,
            Active = model.Active
        };
    }

    private void AddErrors(Result result)
    {
        foreach (Error error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Message);
        }
    }
}
