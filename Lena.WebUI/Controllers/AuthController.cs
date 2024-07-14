using Business.Abstract;
using Entities.Dto;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Controllers;
[Route("auth")]
public class AuthController : Controller
{
    private readonly IUserService _userService;
    private readonly IValidator<UserForRegisterDto> _validator;

    public AuthController(IUserService userService, IValidator<UserForRegisterDto> validator)
    {
        _userService = userService;
        _validator = validator;
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserForRegisterDto model)
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
        var result = await _userService.Register(model);
        if (!result.Success)
            return BadRequest(result.Message);

        var claims = new List<Claim>
        {
            new Claim("UserName", result.Data.UserName),
            new Claim(ClaimTypes.Email, result.Data.Email),
            new Claim(ClaimTypes.Name, $"{result.Data.FirstName} {result.Data.LastName}"),
            new Claim(ClaimTypes.Role, "User"),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var props = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTime.UtcNow.AddDays(1),
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);



        return RedirectToAction("/");
    }


    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserForLoginDto model)
    {
        var result = await _userService.Login(model);
        if (!result.Success)
            return BadRequest(result.Message);

        var claims = new List<Claim>
        {
            new Claim("UserName", result.Data.UserName),
            new Claim(ClaimTypes.Email, result.Data.Email),
            new Claim(ClaimTypes.Name, $"{result.Data.FirstName} {result.Data.LastName}"),
            new Claim(ClaimTypes.Role, "User"),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var props = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTime.UtcNow.AddDays(1),
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);

        return RedirectToAction("Index", "Home");

    }
}
