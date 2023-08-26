using Application.AppServices;
using Application.Interfaces;
using Application.Objects.Requests.Usuario;
using Application.Services;
using Domain.External.Interfaces.Services;
using FluentValidation;
using Infra.Auth;
using Infra.Data.Interfaces;
using Infra.Data.Repositories;
using Infra.External.Services;
using Microsoft.Extensions.DependencyInjection;
using static Application.Objects.Requests.Usuario.AddCustomerRequest;

namespace CrossCuting.Native_Injector;

public static class NativeInjector
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<ICustomerAppService, CustomerAppService>();
        services.AddScoped<ILocationAppService, LocationAppService>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICustomerService, CustomerService>();

        services.AddScoped<IViacepExternalService, ViacepExternalService>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddScoped<IValidator<AddCustomerRequest>, SaveCustomerRequestValidator>();
    }
}