using Entities.Dto;
using Entities.Models;

namespace Business.Abstract;

public interface IFormService
{
    Task<ResponseModel<FormDto>> AddForm(FormDto model);
}
