using Microsoft.Extensions.DependencyInjection;
using SMS.Application.Services;
using SMS.Application.UseCases.People.Commands.CreatePerson;
using SMS.Application.UseCases.People.Commands.UpdatePerson;
using SMS.Application.UseCases.People.Queries;

namespace SMS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<PersonService>();
        services.AddScoped<AddPersonUseCase>();
        services.AddScoped<UpdatePersonUseCase>();
        services.AddScoped<GetPersonUseCase>();
        services.AddScoped<GetPersonImage>();

        return services;
    }

    public static void Mappings(IServiceCollection services)
    {
        // for auto mapper 
    }
}