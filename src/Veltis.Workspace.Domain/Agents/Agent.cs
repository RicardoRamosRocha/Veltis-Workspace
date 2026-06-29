using Veltis.Workspace.Domain.Common;
using Veltis.Workspace.Domain.Entities;

namespace Veltis.Workspace.Domain.Agents;

public sealed class Agent : TenantEntity
{
    public Guid ProfessionId { get; set; }
    public Profession? Profession { get; set; }
    public Guid CategoryId { get; set; }
    public AgentCategory? Category { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public string? CoverImage { get; set; }
    public Guid PromptTemplateId { get; set; }
    public PromptTemplate? PromptTemplate { get; set; }
    public Guid FormDefinitionId { get; set; }
    public FormDefinition? FormDefinition { get; set; }
    public Guid? DefaultModelId { get; set; }
    public AIModel? DefaultModel { get; set; }
    public AgentExecutionMode ExecutionMode { get; set; } = AgentExecutionMode.Standard;
    public decimal Temperature { get; set; } = 0.2m;
    public int MaxTokens { get; set; } = 2000;
    public bool IsFeatured { get; set; }
    public bool IsPublic { get; set; } = true;
    public bool RequiresSubscription { get; set; }
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }
    public int Version { get; set; } = 1;
}
