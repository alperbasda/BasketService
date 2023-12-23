using Core.Persistence.Repositories;

namespace Core.Persistence.Models;

public interface IEntity<TId> : IEntityTimestamps
{
    TId Id { get; set; }
}

public class Entity<TId> : IEntity<TId>
{
    public TId Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public DateTime? DeletedTime { get; set; }

    public Entity()
    {
        Id = default;
    }

    public Entity(TId id)
    {
        Id = id;
    }
}
