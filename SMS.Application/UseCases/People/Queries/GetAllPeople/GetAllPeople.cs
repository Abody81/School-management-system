using SMS.Application.Interfaces.Repositories;
using SMS.Domain.Entities;
using SMS.Domain.Enums;
using SMS.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Application.UseCases.People.Queries.GetAllPeople;

public class GetAllPeople(IPersonRepository _repo)
{
    public async Task<Result<List<Person>>> ExecuteAsync(GetAllPeopleQuery query, CancellationToken ct)
    {
        if (query.pageSize <= 0)
        {
            return PersonErrors.InvalidPageSize;
        }

        if (query.lastId < 0)
        {
            return PersonErrors.InvalidId;
        }

        return await _repo.GetAllPeople(query.lastId, query.pageSize, ct);
    }
}
