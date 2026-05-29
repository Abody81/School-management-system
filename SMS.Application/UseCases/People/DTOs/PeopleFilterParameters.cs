namespace SMS.Application.DTOs;

public record PeopleFilterParameters(
 int LastId = 0,
 int PageSize = 10,
 int? PersonId = null,
 string? NationalNumber = null,
 string? FirstName = null,
 string? SecondName = null,
 string? ThirdName = null,
 string? LastName = null);
