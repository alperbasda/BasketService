namespace Core.Persistence.Repositories;

public interface IEntityTimestamps
{
    DateTime CreatedTime { get; set; }
    DateTime? UpdatedTime { get; set; }
    DateTime? DeletedTime { get; set; }

}
