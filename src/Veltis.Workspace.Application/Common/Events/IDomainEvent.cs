namespace Veltis.Workspace.Application.Common.Events;

public interface IDomainEvent
{
    DateTime OccurredAt { get; }
}
