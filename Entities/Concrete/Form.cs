using Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Entities.Concrete;
public class Form : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class Field
{
    public int Id { get; set; }
    public int FormId { get; set; }
    public string Name { get; set; }
    public bool Required { get; set; }

}
