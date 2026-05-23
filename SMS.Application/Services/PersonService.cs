using SMS.Application.UseCases.People.Commands.CreatePerson;
using SMS.Application.UseCases.People.Commands.UpdatePerson;
using SMS.Application.UseCases.People.Queries;

namespace SMS.Application.Services;

public class PersonService
{
    public AddPersonUseCase Add { get; }
    public UpdatePersonUseCase Update { get; }
    public GetPersonUseCase Get { get; }
    public GetPersonImage GetImage { get; }

    public PersonService(
        AddPersonUseCase add,
        UpdatePersonUseCase update,
        GetPersonUseCase get,
        GetPersonImage getImage
        )

    {
        Add = add;
        Update = update;
        Get = get;
        GetImage = getImage;
    }
}
