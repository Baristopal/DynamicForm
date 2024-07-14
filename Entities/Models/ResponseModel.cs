using System.Text.Json.Serialization;

namespace Entities.Models;
public class ResponseModel<T>
{
    public string Message { get; set; }
    public bool Success { get; set; }
    public T Data { get; set; }

    public ResponseModel()
    {

    }

    public ResponseModel(T data, bool success)
    {
        Data = data;
        Success = success;
    }
    public ResponseModel(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public ResponseModel(T data, bool success, string message) : this(data, success)
    {
        Message = message;
    }
}


public class ResponseModel
{
    public bool Success { get; set; }
    public string Message { get; set; }
    [JsonConstructor]
    public ResponseModel(bool success, string message = "")
    {
        Success = success;
        Message = message;
    }
}
