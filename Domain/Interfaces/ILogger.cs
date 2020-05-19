namespace Domain.Interfaces
{
    public interface ILogger
    {   
        int EntityId { get; }
        string EntityIdentifier { get; }
        string Type { get; }
        string MessageWhenCreated { get; }
        string MessageWhenUpdated { get; }
        string MessageWhenDeleted { get; }
    }
}
