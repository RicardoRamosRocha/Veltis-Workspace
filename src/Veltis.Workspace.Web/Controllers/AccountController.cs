using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Application.Professions;
using Veltis.Workspace.Application.Workspaces;
using Veltis.Workspace.Domain.Entities;
using Veltis.Workspace.Domain.Identity;
using Veltis.Workspace.Web.Models.Account;

namespace Veltis.Workspace.Web.Controllers;

[Route("account")]
public sealed class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IProfessionService _professionService;
    private readonly IWorkspaceService _workspaceService;
    private readonly IApplicationDbContext _context;

    public AccountController(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IProfessionService professionService,
        IWorkspaceService workspaceService,
        IApplicationDbContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _professionService = professionService;
        _workspaceService = workspaceService;
        _context = context;
    }

    [HttpGet("login")]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        ApplicationUser? user = await _userManager.FindByEmailAsync(model.Email);
        if (user is null || !user.IsActive)
        {
            ModelState.AddModelError(string.Empty, "Credenciais invalidas.");
            return View(model);
        }

        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(
            user,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: true);

        if (result.Succeeded)
        {
            user.LastLoginAt = DateTimeOffset.UtcNow;
            await _userManager.UpdateAsync(user);
            await _workspaceService.EnsureForUserAsync(user.Id, user.DisplayName ?? user.Email ?? "Usuario");

            if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Credenciais invalidas.");
        return View(model);
    }

    [HttpGet("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register()
    {
        await PopulateProfessionsAsync();
        return View(new RegisterViewModel());
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateProfessionsAsync();
            return View(model);
        }

        bool professionExists = await _context.Professions.AnyAsync(
            profession => profession.Id == model.ProfessionId && profession.Active && !profession.IsDeleted);

        if (!professionExists || model.ProfessionId is null)
        {
            ModelState.AddModelError(nameof(model.ProfessionId), "Selecione uma profissao valida.");
            await PopulateProfessionsAsync();
            return View(model);
        }

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            DisplayName = model.DisplayName,
            ProfessionId = model.ProfessionId
        };

        IdentityResult result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            _context.UserProfessions.Add(new UserProfession
            {
                UserId = user.Id,
                ProfessionId = model.ProfessionId.Value,
                IsPrimary = true
            });
            await _context.SaveChangesAsync();
            await _workspaceService.EnsureForUserAsync(user.Id, user.DisplayName ?? user.Email ?? "Usuario");
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Dashboard");
        }

        foreach (IdentityError error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        await PopulateProfessionsAsync();
        return View(model);
    }

    [HttpPost("logout")]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Dashboard");
    }

    [HttpGet("access-denied")]
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }

    private async Task PopulateProfessionsAsync()
    {
        IReadOnlyCollection<ProfessionDto> professions = await _professionService.GetActiveAsync();
        ViewBag.Professions = professions
            .Select(profession => new SelectListItem(profession.Name, profession.Id.ToString()))
            .ToList();
    }
}
