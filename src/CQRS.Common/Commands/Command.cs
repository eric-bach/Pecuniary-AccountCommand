using System;

namespace CQRS.Common.Commands
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
