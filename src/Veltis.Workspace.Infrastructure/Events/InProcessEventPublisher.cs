using Microsoft.Extensions.DependencyInjection;
using Veltis.Workspace.Application.Common.Events;

namespace Veltis.Workspace.Infrastructure.Events;

public sealed class InProcessEventPublisher : IEventPublisher
{
    private readonly IServiceProvider _serviceProvider;

    public InProcessEventPublisher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class
    {
        IEnumerable<IEventSubscriber<TEvent>> subscribers = _serviceProvider.GetServices<IEventSubscriber<TEvent>>();

        foreach (IEventSubscriber<TEvent> subscriber in subscribers)
        {
            await subscriber.HandleAsync(@event, cancellationToken);
        }
    }
}
