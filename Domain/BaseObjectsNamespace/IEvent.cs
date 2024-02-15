namespace Domain.BaseObjectsNamespace
{
    public interface IEvent
    {
        Guid Id { get; }
        DateTime Date { get; }
    }
}
