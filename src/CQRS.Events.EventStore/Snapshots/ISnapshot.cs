namespace CQRS.Events.EventStore.Snapshots
{
    public interface ISnapshot
    {
        Snapshot GetSnapshot();
        void SetSnapshot(Snapshot snapshot);
    }
}
