using Entities.Abstract;

namespace Entities.Dto;
public class FormDto : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<FieldDto> Fields { get; set; }
}
