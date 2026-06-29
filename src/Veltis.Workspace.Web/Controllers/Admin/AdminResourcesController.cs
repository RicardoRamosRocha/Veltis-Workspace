using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Veltis.Workspace.Application.Admin;
using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Domain.Agents;
using Veltis.Workspace.Domain.Billing;
using Veltis.Workspace.Domain.Common;
using Veltis.Workspace.Domain.Constants;
using Veltis.Workspace.Domain.FeatureFlags;
using Veltis.Workspace.Domain.Tenancy;
using Veltis.Workspace.Web.Models.Admin;

namespace Veltis.Workspace.Web.Controllers.Admin;

[Authorize(Roles = ApplicationRoles.AdminAccess)]
[Route("admin")]
public sealed class AdminResourcesController : Controller
{
    private const int PageSize = 10;
    private readonly IApplicationDbContext _context;
    private readonly AdminFormValidator _validator = new();

    public AdminResourcesController(IApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("/AdminResources")]
    [HttpGet("/AdminResources/Index")]
    public IActionResult LegacyIndex()
    {
        return Redirect("/admin");
    }

    [HttpGet("{resource}")]
    public async Task<IActionResult> Index(string resource, string? search, int page = 1)
    {
        ResourceDefinition? definition = GetDefinition(resource);
        if (definition is null)
        {
            return NotFound();
        }

        List<object> allItems = await definition.LoadAsync(_context);
        IEnumerable<object> filtered = allItems;

        if (!string.IsNullOrWhiteSpace(search))
        {
            filtered = filtered.Where(item => definition.SearchFields.Any(field =>
                (GetValue(item, field)?.ToString() ?? string.Empty).Contains(search, StringComparison.OrdinalIgnoreCase)));
        }

        filtered = filtered.OrderBy(item => GetValue(item, definition.SortField)?.ToString());

        int totalItems = filtered.Count();
        int currentPage = Math.Max(1, page);
        List<AdminResourceRowViewModel> rows = filtered
            .Skip((currentPage - 1) * PageSize)
            .Take(PageSize)
            .Select(item => new AdminResourceRowViewModel
            {
                Id = ((BaseEntity)item).Id,
                CanToggle = definition.ToggleField is not null,
                Cells = definition.ListFields.Select(field => FormatDisplay(GetValue(item, field))).ToArray()
            })
            .ToList();

        return View("Index", new AdminResourceListViewModel
        {
            Resource = resource,
            Title = definition.Title,
            Search = search ?? string.Empty,
            Page = currentPage,
            PageSize = PageSize,
            TotalItems = totalItems,
            Headers = definition.ListHeaders,
            Rows = rows
        });
    }

    [HttpGet("{resource}/details/{id:guid}")]
    public async Task<IActionResult> Details(string resource, Guid id)
    {
        ResourceDefinition? definition = GetDefinition(resource);
        object? item = definition is null ? null : await definition.FindAsync(_context, id);
        if (definition is null || item is null)
        {
            return NotFound();
        }

        AdminResourceFormViewModel model = await BuildFormModelAsync(definition, item);
        return View("Details", model);
    }

    [HttpGet("{resource}/create")]
    public async Task<IActionResult> Create(string resource)
    {
        ResourceDefinition? definition = GetDefinition(resource);
        if (definition is null)
        {
            return NotFound();
        }

        object item = Activator.CreateInstance(definition.EntityType)
            ?? throw new InvalidOperationException("Não foi possível criar o recurso administrativo.");
        return View("Form", await BuildFormModelAsync(definition, item));
    }

    [HttpPost("{resource}/create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string resource, AdminResourceFormViewModel model)
    {
        ResourceDefinition? definition = GetDefinition(resource);
        if (definition is null)
        {
            return NotFound();
        }

        Dictionary<string, string?> values = model.Values;
        AdminValidationResult validation = _validator.Validate(definition.Fields, values);
        object item = Activator.CreateInstance(definition.EntityType)
            ?? throw new InvalidOperationException("Não foi possível criar o recurso administrativo.");

        if (!validation.IsValid)
        {
            AddErrors(validation);
            AdminResourceFormViewModel invalidModel = await BuildFormModelAsync(definition, item, values);
            return View("Form", invalidModel);
        }

        ApplyValues(item, definition, values);
        definition.Add(_context, item);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index), new { resource });
    }

    [HttpGet("{resource}/edit/{id:guid}")]
    public async Task<IActionResult> Edit(string resource, Guid id)
    {
        ResourceDefinition? definition = GetDefinition(resource);
        object? item = definition is null ? null : await definition.FindAsync(_context, id);
        if (definition is null || item is null)
        {
            return NotFound();
        }

        return View("Form", await BuildFormModelAsync(definition, item));
    }

    [HttpPost("{resource}/edit/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string resource, Guid id, AdminResourceFormViewModel model)
    {
        ResourceDefinition? definition = GetDefinition(resource);
        object? item = definition is null ? null : await definition.FindAsync(_context, id);
        if (definition is null || item is null)
        {
            return NotFound();
        }

        Dictionary<string, string?> values = model.Values;
        AdminValidationResult validation = _validator.Validate(definition.Fields, values);
        if (!validation.IsValid)
        {
            AddErrors(validation);
            return View("Form", await BuildFormModelAsync(definition, item, values));
        }

        ApplyValues(item, definition, values);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index), new { resource });
    }

    [HttpPost("{resource}/toggle/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(string resource, Guid id)
    {
        ResourceDefinition? definition = GetDefinition(resource);
        object? item = definition is null ? null : await definition.FindAsync(_context, id);
        if (definition?.ToggleField is null || item is null)
        {
            return NotFound();
        }

        PropertyInfo? property = definition.EntityType.GetProperty(definition.ToggleField);
        if (property?.PropertyType == typeof(bool))
        {
            bool current = (bool)(property.GetValue(item) ?? false);
            property.SetValue(item, !current);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index), new { resource });
    }

    [HttpGet("{resource}/preview/{id:guid}")]
    public async Task<IActionResult> Preview(string resource, Guid id, [FromServices] Veltis.Workspace.Application.Forms.Interfaces.IFormRenderer renderer)
    {
        if (!resource.Equals("form-definitions", StringComparison.OrdinalIgnoreCase))
        {
            return NotFound();
        }

        FormDefinition? form = await _context.FormDefinitions.FindAsync(id);
        if (form is null)
        {
            return NotFound();
        }

        string schema = string.IsNullOrWhiteSpace(form.SchemaJson) || form.SchemaJson == "{}" ? form.JsonSchema : form.SchemaJson;
        return View("Preview", renderer.Render(schema, form.UiSchemaJson, isPreview: true));
    }

    private async Task<AdminResourceFormViewModel> BuildFormModelAsync(
        ResourceDefinition definition,
        object item,
        Dictionary<string, string?>? values = null)
    {
        Dictionary<string, string?> formValues = values ?? definition.Fields.ToDictionary(
            field => field.Name,
            field => FormatEditValue(GetValue(item, field.Name)));

        List<AdminResourceFieldViewModel> fields = [];
        foreach (AdminFieldSpec field in definition.Fields)
        {
            fields.Add(new AdminResourceFieldViewModel
            {
                Spec = field,
                Options = await GetOptionsAsync(field.Name)
            });
        }

        return new AdminResourceFormViewModel
        {
            Resource = definition.Resource,
            Title = definition.Title,
            Id = ((BaseEntity)item).Id,
            Values = formValues,
            Fields = fields
        };
    }

    private async Task<IReadOnlyList<SelectListItem>> GetOptionsAsync(string fieldName)
    {
        return fieldName switch
        {
            "ProfessionId" => await _context.Professions.OrderBy(item => item.Name)
                .Select(item => new SelectListItem(item.Name, item.Id.ToString())).ToListAsync(),
            "CategoryId" => await _context.AgentCategories.OrderBy(item => item.Name)
                .Select(item => new SelectListItem(item.Name, item.Id.ToString())).ToListAsync(),
            "PromptTemplateId" => await _context.PromptTemplates.OrderBy(item => item.SystemPrompt)
                .Select(item => new SelectListItem(item.SystemPrompt.Length > 80 ? item.SystemPrompt.Substring(0, 80) : item.SystemPrompt, item.Id.ToString())).ToListAsync(),
            "FormDefinitionId" => await _context.FormDefinitions.OrderBy(item => item.Name)
                .Select(item => new SelectListItem(item.Name, item.Id.ToString())).ToListAsync(),
            "DefaultModelId" or "AIProviderId" => await GetModelOrProviderOptionsAsync(fieldName),
            "TenantId" => await _context.Tenants.OrderBy(item => item.Name)
                .Select(item => new SelectListItem(item.Name, item.Id.ToString())).ToListAsync(),
            _ => []
        };
    }

    private async Task<IReadOnlyList<SelectListItem>> GetModelOrProviderOptionsAsync(string fieldName)
    {
        if (fieldName == "AIProviderId")
        {
            return await _context.AIProviders.OrderBy(item => item.Name)
                .Select(item => new SelectListItem(item.Name, item.Id.ToString())).ToListAsync();
        }

        return await _context.AIModels.OrderBy(item => item.ModelName)
            .Select(item => new SelectListItem(item.ModelName, item.Id.ToString())).ToListAsync();
    }

    private static void ApplyValues(object item, ResourceDefinition definition, IReadOnlyDictionary<string, string?> values)
    {
        foreach (AdminFieldSpec field in definition.Fields)
        {
            PropertyInfo? property = definition.EntityType.GetProperty(field.Name);
            if (property is null || !property.CanWrite)
            {
                continue;
            }

            values.TryGetValue(field.Name, out string? rawValue);
            property.SetValue(item, ConvertValue(rawValue, property.PropertyType));
        }
    }

    private static object? ConvertValue(string? rawValue, Type propertyType)
    {
        Type targetType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
        string value = rawValue?.Trim() ?? string.Empty;

        if (targetType == typeof(string))
        {
            return value;
        }

        if (targetType == typeof(bool))
        {
            return value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                   value.Equals("on", StringComparison.OrdinalIgnoreCase);
        }

        if (string.IsNullOrWhiteSpace(value))
        {
            return Nullable.GetUnderlyingType(propertyType) is not null ? null : Activator.CreateInstance(targetType);
        }

        if (targetType == typeof(Guid))
        {
            return Guid.Parse(value);
        }

        if (targetType.IsEnum)
        {
            return Enum.Parse(targetType, value);
        }

        if (targetType == typeof(int))
        {
            return int.Parse(value, CultureInfo.InvariantCulture);
        }

        if (targetType == typeof(decimal))
        {
            return decimal.Parse(value, CultureInfo.InvariantCulture);
        }

        return Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
    }

    private static string FormatDisplay(object? value)
    {
        return value switch
        {
            null => "-",
            bool boolean => boolean ? "Sim" : "Não",
            decimal money => money.ToString("0.##", CultureInfo.InvariantCulture),
            DateTime date => date.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
            _ => value.ToString() ?? "-"
        };
    }

    private static string? FormatEditValue(object? value)
    {
        return value switch
        {
            null => null,
            bool boolean => boolean ? "true" : "false",
            decimal decimalValue => decimalValue.ToString(CultureInfo.InvariantCulture),
            DateTime date => date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            _ => value.ToString()
        };
    }

    private static object? GetValue(object item, string propertyName)
    {
        return item.GetType().GetProperty(propertyName)?.GetValue(item);
    }

    private void AddErrors(AdminValidationResult validation)
    {
        foreach (KeyValuePair<string, List<string>> error in validation.Errors)
        {
            foreach (string message in error.Value)
            {
                ModelState.AddModelError($"Values[{error.Key}]", message);
            }
        }
    }

    private ResourceDefinition? GetDefinition(string resource)
    {
        return Definitions().FirstOrDefault(item => item.Resource.Equals(resource, StringComparison.OrdinalIgnoreCase));
    }

    private List<ResourceDefinition> Definitions()
    {
        return
        [
            new("agent-categories", "Categorias de Agentes", typeof(AgentCategory),
                context => context.AgentCategories.Cast<object>().ToListAsync(),
                (context, id) => context.AgentCategories.FindAsync(id).AsTask().ContinueWith(task => (object?)task.Result),
                (context, item) => context.AgentCategories.Add((AgentCategory)item),
                ["Name", "Slug", "IsActive"],
                ["Nome", "Slug", "Ativa"],
                "Name",
                ["Name", "Slug"],
                "IsActive",
                Text("Name", "Nome", true), Slug(), TextArea("Description", "Descrição"), Text("Icon", "Ícone"), Number("DisplayOrder", "Ordem"), Bool("IsActive", "Ativa")),

            new("agents", "Agentes", typeof(Agent),
                context => context.Agents.Cast<object>().ToListAsync(),
                (context, id) => context.Agents.FindAsync(id).AsTask().ContinueWith(task => (object?)task.Result),
                (context, item) => context.Agents.Add((Agent)item),
                ["Name", "Slug", "IsActive", "Version"],
                ["Nome", "Slug", "Ativo", "Versão"],
                "Name",
                ["Name", "Slug"],
                "IsActive",
                Select("ProfessionId", "Profissão", true), Select("CategoryId", "Categoria", true), Text("Name", "Nome", true), Slug(),
                TextArea("Description", "Descrição"), Text("Icon", "Ícone"), Text("CoverImage", "Imagem de capa"), Select("PromptTemplateId", "Prompt", true),
                Select("FormDefinitionId", "Formulário", true), Select("DefaultModelId", "Modelo padrão"), EnumField("ExecutionMode", "Modo de execução"),
                Decimal("Temperature", "Temperature", 0, 2), Number("MaxTokens", "MaxTokens", 1), Bool("IsFeatured", "Destaque"),
                Bool("IsPublic", "Público"), Bool("RequiresSubscription", "Requer assinatura"), Bool("IsActive", "Ativo"), Number("DisplayOrder", "Ordem"), Number("Version", "Versão", 1)),

            new("prompts", "Prompts", typeof(PromptTemplate),
                context => context.PromptTemplates.Cast<object>().ToListAsync(),
                (context, id) => context.PromptTemplates.FindAsync(id).AsTask().ContinueWith(task => (object?)task.Result),
                (context, item) => context.PromptTemplates.Add((PromptTemplate)item),
                ["SystemPrompt", "Language", "Version"],
                ["Prompt", "Idioma", "Versão"],
                "SystemPrompt",
                ["SystemPrompt", "Language", "Tone"],
                null,
                TextArea("SystemPrompt", "System Prompt", true), TextArea("Instructions", "Instruções"), Json("Variables", "Variáveis"),
                TextArea("OutputFormat", "Formato de saída"), Text("Language", "Idioma", true), Text("Tone", "Tom"), Number("Version", "Versão", 1)),

            new("form-definitions", "Formulários Dinâmicos", typeof(FormDefinition),
                context => context.FormDefinitions.Cast<object>().ToListAsync(),
                (context, id) => context.FormDefinitions.FindAsync(id).AsTask().ContinueWith(task => (object?)task.Result),
                (context, item) => context.FormDefinitions.Add((FormDefinition)item),
                ["Name", "Category", "Version", "IsPublished"],
                ["Nome", "Categoria", "Versão", "Publicado"],
                "Name",
                ["Name", "Category"],
                "IsPublished",
                Text("Name", "Nome", true), TextArea("Description", "Descrição"), Json("SchemaJson", "Schema JSON", true), Json("UiSchemaJson", "UI Schema JSON"),
                Json("ValidationJson", "Validation JSON"), Text("Category", "Categoria"), Text("Icon", "Ícone"), Number("Version", "Versão", 1), Bool("IsPublished", "Publicado")),

            new("ai-providers", "Providers de IA", typeof(AIProvider),
                context => context.AIProviders.Cast<object>().ToListAsync(),
                (context, id) => context.AIProviders.FindAsync(id).AsTask().ContinueWith(task => (object?)task.Result),
                (context, item) => context.AIProviders.Add((AIProvider)item),
                ["Name", "Key", "IsActive"],
                ["Nome", "Chave", "Ativo"],
                "Name",
                ["Name", "Key"],
                "IsActive",
                Text("Name", "Nome", true), Text("Key", "Chave", true), Bool("IsActive", "Ativo")),

            new("ai-models", "Modelos de IA", typeof(AIModel),
                context => context.AIModels.Cast<object>().ToListAsync(),
                (context, id) => context.AIModels.FindAsync(id).AsTask().ContinueWith(task => (object?)task.Result),
                (context, item) => context.AIModels.Add((AIModel)item),
                ["Provider", "ModelName", "IsActive"],
                ["Provider", "Modelo", "Ativo"],
                "ModelName",
                ["Provider", "ModelName"],
                "IsActive",
                Select("AIProviderId", "Provider", true), Text("Provider", "Provider", true), Text("ModelName", "Modelo", true), Number("ContextWindow", "Janela de contexto", 1),
                Bool("SupportsVision", "Vision"), Bool("SupportsFunctions", "Functions"), Bool("SupportsStreaming", "Streaming"), Number("MaxTokens", "MaxTokens", 1),
                Decimal("InputPrice", "Preço de entrada", 0), Decimal("OutputPrice", "Preço de saída", 0), Bool("IsActive", "Ativo")),

            new("tenants", "Tenants", typeof(Tenant),
                context => context.Tenants.Cast<object>().ToListAsync(),
                (context, id) => context.Tenants.FindAsync(id).AsTask().ContinueWith(task => (object?)task.Result),
                (context, item) => context.Tenants.Add((Tenant)item),
                ["Name", "Slug", "Active"],
                ["Nome", "Slug", "Ativo"],
                "Name",
                ["Name", "Slug", "Region"],
                "Active",
                Text("Name", "Nome", true), Slug(), Text("Region", "Região"), Bool("Active", "Ativo")),

            new("feature-flags", "Feature Flags", typeof(FeatureFlag),
                context => context.FeatureFlags.Cast<object>().ToListAsync(),
                (context, id) => context.FeatureFlags.FindAsync(id).AsTask().ContinueWith(task => (object?)task.Result),
                (context, item) => context.FeatureFlags.Add((FeatureFlag)item),
                ["TenantId", "FeatureKey", "Enabled"],
                ["Tenant", "Feature", "Ativa"],
                "FeatureKey",
                ["FeatureKey"],
                "Enabled",
                Select("TenantId", "Tenant"), Text("FeatureKey", "Feature Key", true), Bool("Enabled", "Ativa")),

            new("plans", "Planos", typeof(Plan),
                context => context.Plans.Cast<object>().ToListAsync(),
                (context, id) => context.Plans.FindAsync(id).AsTask().ContinueWith(task => (object?)task.Result),
                (context, item) => context.Plans.Add((Plan)item),
                ["Name", "Slug", "MonthlyPrice", "Active"],
                ["Nome", "Slug", "Preço mensal", "Ativo"],
                "Name",
                ["Name", "Slug", "Currency"],
                "Active",
                Text("Name", "Nome", true), Slug(), Decimal("MonthlyPrice", "Preço mensal", 0), Text("Currency", "Moeda", true), Bool("Active", "Ativo"))
        ];
    }

    private static AdminFieldSpec Text(string name, string label, bool required = false) => new() { Name = name, Label = label, Required = required };
    private static AdminFieldSpec TextArea(string name, string label, bool required = false) => new() { Name = name, Label = label, Kind = AdminFieldKind.TextArea, Required = required };
    private static AdminFieldSpec Number(string name, string label, int? min = null) => new() { Name = name, Label = label, Kind = AdminFieldKind.Number, MinInt = min };
    private static AdminFieldSpec Decimal(string name, string label, decimal? min = null, decimal? max = null) => new() { Name = name, Label = label, Kind = AdminFieldKind.Decimal, MinDecimal = min, MaxDecimal = max };
    private static AdminFieldSpec Bool(string name, string label) => new() { Name = name, Label = label, Kind = AdminFieldKind.Boolean };
    private static AdminFieldSpec Select(string name, string label, bool required = false) => new() { Name = name, Label = label, Kind = AdminFieldKind.Select, Required = required };
    private static AdminFieldSpec Json(string name, string label, bool required = false) => new() { Name = name, Label = label, Kind = AdminFieldKind.Json, IsJson = true, Required = required };
    private static AdminFieldSpec Slug() => new() { Name = "Slug", Label = "Slug", Required = true, IsSlug = true };
    private static AdminFieldSpec EnumField(string name, string label) => new() { Name = name, Label = label, Kind = AdminFieldKind.Enum, Required = true };

    private sealed record ResourceDefinition(
        string Resource,
        string Title,
        Type EntityType,
        Func<IApplicationDbContext, Task<List<object>>> LoadAsync,
        Func<IApplicationDbContext, Guid, Task<object?>> FindAsync,
        Action<IApplicationDbContext, object> Add,
        IReadOnlyList<string> ListFields,
        IReadOnlyList<string> ListHeaders,
        string SortField,
        IReadOnlyList<string> SearchFields,
        string? ToggleField,
        params AdminFieldSpec[] Fields);
}
