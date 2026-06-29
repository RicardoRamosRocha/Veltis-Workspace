namespace Veltis.Workspace.Application.Common.Events;

public interface IEventSubscriber<in TEvent>
    where TEvent : class
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}
