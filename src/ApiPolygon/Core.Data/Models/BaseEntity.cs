namespace Core.Data.Models;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    public BaseEntity()
    {
        
    }
}