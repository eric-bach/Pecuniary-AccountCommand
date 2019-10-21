using System;

namespace EricBach.CQRS.Commands
{
    public interface ICommand
    {
        Guid Id { get; }
    }

    public class Command : ICommand
    {
        public Guid Id { get; private set; }

        public Command(Guid aggregateId)
        {
            Id = aggregateId;
        }
    }
}
