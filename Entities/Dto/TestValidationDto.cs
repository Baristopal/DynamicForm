namespace Entities.Dto;
public class TestValidationDto
{
    public int FormId { get; set; }
    public string Name { get; set; }
    public bool Required { get; set; }
    public string DataType { get; set; }
    public object Data { get; set; }
}
