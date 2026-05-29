using SMS.Application.UseCases.People.Commands.CreatePerson;
using SMS.Application.UseCases.People.Commands.DeletePerson;
using SMS.Application.UseCases.People.Commands.UpdatePerson;
using SMS.Application.UseCases.People.Queries;
using SMS.Application.UseCases.People.Queries.GetAllPeople;
using SMS.Application.UseCases.People.Queries.GetAllPeopleFiltered;

namespace SMS.Application.Services;

public class PersonService
{
    public CreatePersonUseCase Add { get; }
    public UpdatePersonUseCase Update { get; }
    public GetPerson Get { get; }
    public GetPersonImage GetImage { get; }
    public DeletePersonUseCase Delete { get; }
    public GetAllPeople GetAll { get; }
    public GetAllPeopleFiltered GetAllFiltered { get; }

    public PersonService(
        CreatePersonUseCase add,
        UpdatePersonUseCase update,
        GetPerson get,
        GetPersonImage getImage,
        DeletePersonUseCase delete,
        GetAllPeople getAll,
        GetAllPeopleFiltered getAllFiltered
        )

    {
        Add = add;
        Update = update;
        Get = get;
        GetImage = getImage;
        Delete = delete;
        GetAll = getAll;
        GetAllFiltered = getAllFiltered;
    }
}
