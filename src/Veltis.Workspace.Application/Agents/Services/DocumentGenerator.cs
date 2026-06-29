using Veltis.Workspace.Application.Agents.Interfaces;
using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Domain.Agents;

namespace Veltis.Workspace.Application.Agents.Services;

public sealed class DocumentGenerator : IDocumentGenerator
{
    private readonly IApplicationDbContext _context;

    public DocumentGenerator(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GeneratedDocument> GenerateAsync(AgentExecution execution, string title, CancellationToken cancellationToken = default)
    {
        var document = new GeneratedDocument
        {
            WorkspaceId = execution.WorkspaceId,
            ExecutionId = execution.Id,
            Title = title,
            Content = execution.Response ?? string.Empty,
            Format = "markdown",
            Status = GeneratedDocumentStatus.Draft
        };

        _context.GeneratedDocuments.Add(document);
        await _context.SaveChangesAsync(cancellationToken);

        return document;
    }
}
