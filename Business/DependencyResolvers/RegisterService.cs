using Business.Abstract;
using Business.AutoMapper;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
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

        ConfigureIoC(services);

    }


    private static void ConfigureIoC(IServiceCollection services)
    {
        services.AddScoped<IUserDal, UserDal>();
        services.AddScoped<IFormDal, FormDal>();



        services.AddScoped<IUserService, UserManager>();
    }

}
