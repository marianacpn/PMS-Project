namespace Domain.Interfaces
{
    public interface ILoggableEntity
    {
        int EntityId { get; }
        string EntityIdentifier { get; }
        string Type { get; }
    }
}
