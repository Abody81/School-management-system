using SMS.Application.DTOs;
using SMS.Application.Interfaces.Repositories;
using SMS.Domain.Entities;
using SMS.Domain.Util;

namespace SMS.Application.UseCases.People.Queries.GetAllPeopleFiltered;

public class GetAllPeopleFiltered(IPersonRepository _repo)
{
    public async Task<Result<List<Person>>> ExecuteAsync(GetAllPeopleFilteredQuery query, CancellationToken ct)
    {
        if (query.PageSize <= 0)
            return PersonErrors.InvalidPageSize;

        if (query.LastId < 0)
            return PersonErrors.InvalidId;

        var parameters = new PeopleFilterParameters(query.LastId, query.PageSize, query.PersonId, query.NationalNumber,
            query.FirstName, query.SecondName, query.ThirdName, query.LastName);
        
        return await _repo.GetAllPeopleFiltered(parameters, ct);
    }
}
