using Entities.Abstract;

namespace Entities.Dto;
public class FormDto : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}
