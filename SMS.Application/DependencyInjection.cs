using Microsoft.Extensions.DependencyInjection;
using SMS.Application.Services;
using SMS.Application.UseCases.People.Commands.CreatePerson;
using SMS.Application.UseCases.People.Commands.DeletePerson;
using SMS.Application.UseCases.People.Commands.UpdatePerson;
using SMS.Application.UseCases.People.Queries;
using SMS.Application.UseCases.People.Queries.GetAllPeople;
using SMS.Application.UseCases.People.Queries.GetAllPeopleFiltered;

namespace SMS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<PersonService>();
        services.AddScoped<CreatePersonUseCase>();
        services.AddScoped<UpdatePersonUseCase>();
        services.AddScoped<GetPerson>();
        services.AddScoped<GetPersonImage>();
        services.AddScoped<DeletePersonUseCase>();
        services.AddScoped<GetAllPeople>();
        services.AddScoped<GetAllPeopleFiltered>();

        return services;
    }

    public static void Mappings(IServiceCollection services)
    {
        // for auto mapper 
    }
}