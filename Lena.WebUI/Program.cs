using Business.DependencyResolvers;
using Business.ValidationRules;
using Entities.Dto;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddFluentValidation(fv => fv?.RegisterValidatorsFromAssemblyContaining<EditFormValidator>());

builder.Services.ConfigureService(builder.Configuration);



builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "AuthCookie";
        options.LoginPath = "/auth/login";
        options.LogoutPath = "/auth/logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.SlidingExpiration = true;
        options.ReturnUrlParameter = "returnUrl";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UsePathBase("/");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Eklenmesi gereken middleware
app.UseAuthorization();

app.MapControllerRoute(
        name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/forms");
        return;
    }

    await next();
});
    

app.Run();
