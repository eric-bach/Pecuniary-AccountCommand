using System;
using EricBach.CQRS.Aggregate;

namespace EricBach.CQRS.Repository
{
    public interface IEventRepository<T> where T : AggregateRoot, new()
    {
        void Save(AggregateRoot aggregate, int expectedVersion);

        T GetById(Guid id);

        void DeleteAll();
    }
}
