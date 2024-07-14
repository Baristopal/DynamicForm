using AutoMapper;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Business.Concrete;
public class FormManager : IFormService
{
    private readonly IFormDal _formDal;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public FormManager(IHttpContextAccessor httpContextAccessor, IFormDal formDal, IMapper mapper)
    {
        _httpContextAccessor = httpContextAccessor;
        _formDal = formDal;
        _mapper = mapper;
    }

    public async Task<ResponseModel<FormDto>> AddForm(FormDto model)
    {

        try
        {
            var form = _mapper.Map<Form>(model);
            _ = int.TryParse(_httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int createdBy);
            form.CreatedBy = createdBy;

            var result = await _formDal.Add(form);
            foreach (var item in model.Fields)
            {
                var field = _mapper.Map<Field>(item);
                field.FormId = result.Id;
                await _formDal.AddField(field);
            }

            return new ResponseModel<FormDto>(_mapper.Map<FormDto>(result), true);
        }
        catch (Exception ex)
        {
            return new ResponseModel<FormDto>(false, ex.Message);
        }
    }



}
