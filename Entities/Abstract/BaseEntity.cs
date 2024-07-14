namespace Entities.Abstract;
public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; } = true;


}
