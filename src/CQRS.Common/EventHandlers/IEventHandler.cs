using CQRS.Common.Events;

namespace CQRS.Common.EventHandlers
{
    public interface IEventHandler<TEvent> where TEvent : Event
    {
        void Handle(TEvent @event);
    }
}
