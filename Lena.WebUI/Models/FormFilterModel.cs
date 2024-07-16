using Entities.Dto;

namespace WebUI.Models;

public class FormFilterModel
{
    public string Name { get; set; }
    public List<FormDto> Forms { get; set; }
}
