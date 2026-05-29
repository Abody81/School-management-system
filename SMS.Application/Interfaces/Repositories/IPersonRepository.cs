using SMS.Application.DTOs;
using SMS.Domain.Entities;

namespace SMS.Application.Interfaces.Repositories;

public interface IPersonRepository
{
    Task<Person?> GetPersonById(int id, CancellationToken ct, bool tracking = false);

    Task<List<Person>> GetAllPeople(int lastId, int pageSize, CancellationToken ct);

    void AddPerson(Person person);

    void UpdatePerson(Person person);

    Task<bool> IsNationalNumberExist(string nationalNumber, CancellationToken ct);

    Task<bool> IsPhoneNumberExist(string? phoneNumber, CancellationToken ct);

    Task<bool> IsNationalNumberReserved(int personID, string nationalNumber, CancellationToken ct);

    Task<bool> IsPhoneNumberReserved(int personID, string? phoneNumber, CancellationToken ct);

    Task<bool> IsPersonExists(int personId);

    Task<int> Delete(int personId, CancellationToken ct);

    /// <remarks>
    /// In this operation you MUST call UnitOfWork.SaveChangesAsync() afterwards to persist changes.
    /// </remarks>
    void StagedDelete(Person person);

    Task<string?> GetImagePath(int personId, CancellationToken ct);

    Task<List<Person>> GetAllPeopleFiltered(PeopleFilterParameters filters, CancellationToken ct);
}