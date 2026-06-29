using Microsoft.Extensions.Logging;
using Veltis.Workspace.Application.Common.Interfaces;

namespace Veltis.Workspace.Infrastructure.Services;

public sealed class SmtpEmailService : IEmailService, IEmailSender
{
    private readonly ILogger<SmtpEmailService> _logger;

    public SmtpEmailService(ILogger<SmtpEmailService> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Email queued for {Recipient} with subject {Subject}.", to, subject);
        return Task.CompletedTask;
    }
}
