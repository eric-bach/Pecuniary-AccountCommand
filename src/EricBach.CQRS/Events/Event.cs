using System;

namespace EricBach.CQRS.Events
{
    public interface IEvent
    {
        Guid Id { get; }
        int Version { get; set; }
    }

    public class Event : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string EventName { get; set; }

        public Event(string eventName)
        {
            EventName = eventName;
        }
    }
}
