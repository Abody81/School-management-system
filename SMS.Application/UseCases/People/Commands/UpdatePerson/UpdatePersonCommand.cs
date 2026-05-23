namespace SMS.Application.UseCases.People.Commands.CreatePerson;

public record class UpdatePersonCommand
    (
    int  Id,
        string FirstName,
      string SecondName,
      string ThirdName,
     string LastName,
     char Gender,
     DateTime DateOfBirth,
    string NationalNumber,
    string? PhoneNumber,
    int CountryID,
    string Address,
    Stream? Image);

