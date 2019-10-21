using EricBach.CQRS.Events;

namespace EricBach.CQRS.EventHandlers
{
    public interface IEventHandler<TEvent> where TEvent : Event
    {
        void Handle(TEvent @event);
    }
}
