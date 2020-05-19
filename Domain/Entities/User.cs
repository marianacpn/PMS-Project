using Domain.Common;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class User : AuditableEntity, ILoggableEntity
    {
        public int EntityId { get; private set; }

        public string EntityIdentifier { get; private set; }

        public string Type { get; private set; }
    }
}
