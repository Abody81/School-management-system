using SMS.Domain.Entities;

namespace SMS.Application.Interfaces.Repositories;

public interface IPersonRepository
{
    Task<Person?> GetPersonById(int id, bool tracking = false, CancellationToken ct = default);
    public void AddPerson(Person person);
    public void UpdatePerson(Person person);
    Task<bool> IsNationalNumberExist(string nationalNumber, CancellationToken ct = default);
    Task<bool> IsPhoneNumberExist(string? phoneNumber, CancellationToken ct = default);
    Task<bool> IsNationalNumberReserved(int personID, string nationalNumber, CancellationToken ct = default);
    Task<bool> IsPhoneNumberReserved(int personID, string? phoneNumber, CancellationToken ct = default);
}