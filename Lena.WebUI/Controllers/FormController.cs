using Business.Abstract;
using Entities.Dto;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;
[Route("/")]
[Authorize(Roles = "User")]
public class FormController : Controller
{
    private readonly IFormService _formService;
    private readonly IValidator<FormDto> _validator;

    public FormController(IFormService formService, IValidator<FormDto> validator)
    {
        _formService = formService;
        _validator = validator;
    }

    [HttpGet("/forms")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("/EditForm")]
    public IActionResult EditForm()
    {
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("~/Views/Form/EditForm.cshtml");

        return BadRequest("Invalid request");
    }

    [HttpPost("/AddForm")]
    public async Task<IActionResult> AddForm([FromBody]FormDto model)
    {
        ValidationResult validationResult = _validator.Validate(model);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return View(model);
        }

        if (model.Fields.Count != 0 == false)
        {
            ModelState.AddModelError("Fields", "Fields cannot be empty");
            return View(model);
        }
        var response = new
        {
            status = 200,
            message = "Form created successfully!"
        };

        //var response = await _formService.AddForm(model);
        if (true)
            return Ok(response);

        return BadRequest(response);
    }
}
