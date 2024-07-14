using Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Entities.Concrete;
public class Form : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}

[Keyless]
public class Field
{
    public int FormId { get; set; }
    public string Name { get; set; }
    public bool Required { get; set; }

}
