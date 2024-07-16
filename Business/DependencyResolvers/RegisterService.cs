using Business.Abstract;
using Business.AutoMapper;
using Business.Concrete;
using Business.ValidationRules;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Dto;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Business.DependencyResolvers;
public static class RegisterService
{

    public static void ConfigureService(this IServiceCollection services, IConfiguration configure)
    {
        var connectionString = configure.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));


        services.AddAutoMapper(opt => opt.AddProfile<AutoMapperProfile>());

        services.AddScoped<IValidator<UserForRegisterDto>, UserRegisterValidator>();
        services.AddScoped<IValidator<FormDto>, EditFormValidator>();
        services.AddScoped<IValidator<UserForLoginDto>, UserLoginValidator>();

        ConfigureIoC(services);

    }


    private static void ConfigureIoC(IServiceCollection services)
    {
        services.AddScoped<IUserDal, UserDal>();
        services.AddScoped<IFormDal, FormDal>();


        services.AddScoped<IFormService, FormManager>();
        services.AddScoped<IUserService, UserManager>();
    }

}
