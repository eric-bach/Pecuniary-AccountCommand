using System;
using CQRS.Common.Aggregate;

namespace CQRS.Events.Repository
{
    public interface IEventRepository<T> where T : AggregateRoot, new()
    {
        void Save(AggregateRoot aggregate, int expectedVersion);

        T GetById(Guid id);

        void DeleteAll();
    }
}
