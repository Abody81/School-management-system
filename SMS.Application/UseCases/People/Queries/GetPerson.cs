using Microsoft.Extensions.Logging;
using SMS.Application.Interfaces.Repositories;
using SMS.Domain.Entities;
using SMS.Domain.Util;

namespace SMS.Application.UseCases.People.Queries;

public class GetPerson(IPersonRepository _repo, ILogger<GetPerson> _logger)
{
    public async Task<Result<Person>> ExecuteAsync(int personId, CancellationToken ct)
    {
        try
        {
            if (personId < 1)
                return Result<Person>.Failure(PersonErrors.InvalidId, new () { { "PersonId", personId } });

            Person? person = await _repo.GetPersonById(personId, ct, false);

            if (person is null)
                return Result<Person>.Failure(PersonErrors.PersonNotFound, new() { { "PersonId", personId } });

            return person;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An internal error occurred while getting the person.");

            return Result<Person>.Failure(PersonErrors.InternalError);
        }
    }

}
