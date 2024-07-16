using Business.Abstract;
using Entities.Dto;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Dynamic;
using WebUI.Models;

namespace WebUI.Controllers;
[Route("/")]
[Authorize(Roles = "User")]
public class FormController : Controller
{
    private readonly IFormService _formService;
    private readonly IValidator<FormDto> _validator;
    private readonly IValidator<TestValidationDto> _fieldValidator;

    public FormController(IFormService formService, IValidator<FormDto> validator, IValidator<TestValidationDto> fieldValidator)
    {
        _formService = formService;
        _validator = validator;
        _fieldValidator = fieldValidator;
    }

    [HttpGet("/forms")]
    public async Task<IActionResult> Index()
    {
        var result = await _formService.GetAllForms();
        if (result.Success == false)
            return BadRequest(result.Message);


        return View(result.Data);
    }

    [HttpGet("/EditForm")]
    public IActionResult EditForm()
    {
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("~/Views/Form/EditForm.cshtml");

        return BadRequest("Invalid request");
    }

    [HttpPost("/AddForm")]
    public async Task<IActionResult> AddForm([FromBody] FormDto model)
    {
        ValidationResult validationResult = _validator.Validate(model);

        if (!validationResult.IsValid)
        {
            var modelState = new ModelStateDictionary();
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            dynamic models = new ExpandoObject();

            models.ModelState = model;
            models.Form = model;

            return Ok(model);
        }

        if (model.Fields.Count == 0)
        {
            ModelState.AddModelError("Fields", "Fields cannot be empty");
            return View(model);
        }

        var response = await _formService.AddForm(model);

        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest("Form could not be created");
    }


    [HttpPost("/SearchForms")]
    public async Task<IEnumerable<FormDto>> SearchForms([FromBody] FormFilterModel filter)
    {
        if (string.IsNullOrEmpty(filter.Name))
            return await Task.FromResult(filter.Forms.Select(s => s));
        var result = await Task.FromResult(filter.Forms.Where(x => x.Name.Contains(filter.Name, StringComparison.InvariantCultureIgnoreCase)));


        return result;
    }


    [HttpGet("/forms/{id}")]
    public async Task<IActionResult> GetFormById(int id)
    {
        var result = await _formService.GetFormById(id);
        if (result.Success == false)
            return BadRequest(result.Message);

        if (result.Data.Fields.Any() == false)
            return Ok(result.Data.Fields);

        return View("FormDetail", result.Data);

    }

    [HttpPost("/FormValidation")]
    public IActionResult FormValidation([FromBody] List<TestValidationDto> models)
    {
        foreach (var item in models)
        {
            if (!item.Required)
                continue;

            var data = item.Data as string;
            if (data != null)
                data = data.Split(":")[1];

            item.Data = data;

            ValidationResult validationResult = _fieldValidator.Validate(item);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

        }
        return View(models);
    }
}
