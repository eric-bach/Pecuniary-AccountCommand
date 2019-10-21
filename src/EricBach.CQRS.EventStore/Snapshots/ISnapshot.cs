namespace EricBach.CQRS.EventStore.Snapshots
{
    public interface ISnapshot
    {
        Snapshot GetSnapshot();
        void SetSnapshot(Snapshot snapshot);
    }
}
