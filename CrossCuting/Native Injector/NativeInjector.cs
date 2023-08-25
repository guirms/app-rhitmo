using Application.AppServices;
using Application.Interfaces;
using Application.Objects.Requests.Usuario;
using Application.Services;
using FluentValidation;
using Infra.Auth;
using Infra.Data.Interfaces;
using Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using static Application.Objects.Requests.Usuario.SaveCustomerRequest;

namespace CrossCuting.Native_Injector;

public static class NativeInjector
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<ICustomerAppService, CustomerAppService>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICustomerService, CustomerService>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddScoped<IValidator<SaveCustomerRequest>, SaveCustomerRequestValidator>();
    }
}