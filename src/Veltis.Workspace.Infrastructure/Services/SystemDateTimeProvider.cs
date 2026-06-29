using Veltis.Workspace.Application.Common.Interfaces;

namespace Veltis.Workspace.Infrastructure.Services;

public sealed class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
