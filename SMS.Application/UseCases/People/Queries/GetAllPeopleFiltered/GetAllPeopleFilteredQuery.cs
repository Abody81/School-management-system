using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Application.UseCases.People.Queries.GetAllPeopleFiltered;

public record GetAllPeopleFilteredQuery
(
 int LastId = 0,
 int PageSize = 10,
 int? PersonId = null,
 string? NationalNumber = null,
 string? FirstName = null,
 string? SecondName = null,
 string? ThirdName = null,
 string? LastName = null
);


