namespace PMS.Domain.Entities;

public abstract class BaseEntity
{
    // Every entity needs a unique identity. So, we can: Find it in DB, Update it, Delete it, etc.
    public Guid Id { get; protected set; } = Guid.NewGuid(); // Guid: Globally unique ID
}