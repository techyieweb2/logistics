using UUIDNext;

namespace Shared.Kernel.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Uuid.NewDatabaseFriendly(Database.SqlServer);
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}