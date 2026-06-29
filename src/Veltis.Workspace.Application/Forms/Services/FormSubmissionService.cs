using System.Text.Json;
using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Application.Forms.Interfaces;
using Veltis.Workspace.Domain.Forms;

namespace Veltis.Workspace.Application.Forms.Services;

public sealed class FormSubmissionService : IFormSubmissionService
{
    private readonly IApplicationDbContext _dbContext;

    public FormSubmissionService(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<FormSubmission> SaveAsync(FormSubmissionRequest request, CancellationToken cancellationToken = default)
    {
        FormSubmission submission = new()
        {
            FormDefinitionId = request.FormDefinitionId,
            UserId = request.UserId,
            WorkspaceId = request.WorkspaceId,
            ValuesJson = JsonSerializer.Serialize(request.Values),
            IsPreview = request.IsPreview
        };

        _dbContext.FormSubmissions.Add(submission);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return submission;
    }
}
