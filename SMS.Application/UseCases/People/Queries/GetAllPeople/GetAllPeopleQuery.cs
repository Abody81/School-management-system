using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Application.UseCases.People.Queries.GetAllPeople;

public record GetAllPeopleQuery(int lastId, int pageSize);



