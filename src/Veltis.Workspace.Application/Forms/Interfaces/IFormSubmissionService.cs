using Veltis.Workspace.Domain.Forms;

namespace Veltis.Workspace.Application.Forms.Interfaces;

public interface IFormSubmissionService
{
    Task<FormSubmission> SaveAsync(FormSubmissionRequest request, CancellationToken cancellationToken = default);
}
