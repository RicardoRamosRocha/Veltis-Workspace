namespace Veltis.Workspace.Application.Common.Events;

public interface IIntegrationEvent
{
    Guid Id { get; }
    DateTime OccurredAt { get; }
}
