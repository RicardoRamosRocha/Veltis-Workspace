namespace Veltis.Workspace.Application.Common.Events;

public interface IApplicationEvent
{
    DateTime OccurredAt { get; }
}
