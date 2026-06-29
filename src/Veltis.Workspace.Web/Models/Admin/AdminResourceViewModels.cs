using Microsoft.AspNetCore.Mvc.Rendering;
using Veltis.Workspace.Application.Admin;

namespace Veltis.Workspace.Web.Models.Admin;

public sealed class AdminResourceListViewModel
{
    public string Resource { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Search { get; set; } = string.Empty;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalItems { get; set; }
    public IReadOnlyList<string> Headers { get; set; } = [];
    public IReadOnlyList<AdminResourceRowViewModel> Rows { get; set; } = [];
    public int TotalPages => TotalItems == 0 ? 1 : (int)Math.Ceiling((double)TotalItems / PageSize);
}

public sealed class AdminResourceRowViewModel
{
    public Guid Id { get; set; }
    public IReadOnlyList<string> Cells { get; set; } = [];
    public bool CanToggle { get; set; }
}

public sealed class AdminResourceFormViewModel
{
    public string Resource { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public Guid? Id { get; set; }
    public Dictionary<string, string?> Values { get; set; } = [];
    public IReadOnlyList<AdminResourceFieldViewModel> Fields { get; set; } = [];
}

public sealed class AdminResourceFieldViewModel
{
    public AdminFieldSpec Spec { get; set; } = new();
    public IReadOnlyList<SelectListItem> Options { get; set; } = [];
}

public sealed class AdminAuditListViewModel
{
    public string? UserId { get; set; }
    public string? Action { get; set; }
    public string? EntityName { get; set; }
    public DateTime? Date { get; set; }
    public IReadOnlyList<AdminAuditRowViewModel> Rows { get; set; } = [];
}

public sealed class AdminAuditRowViewModel
{
    public Guid? UserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;
    public string? EntityId { get; set; }
    public DateTime OccurredAt { get; set; }
}
