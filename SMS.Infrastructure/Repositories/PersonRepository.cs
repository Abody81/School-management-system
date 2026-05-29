using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SMS.Application.DTOs;
using SMS.Application.Interfaces.Repositories;
using SMS.Domain.Entities;
using System.Runtime.CompilerServices;

namespace SMS.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _context;
    public PersonRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Person?> GetPersonById(int id, CancellationToken ct, bool tracking = false)
    {
        var Query = _context.People.AsQueryable();

        if (!tracking) Query = Query.AsNoTracking();

        return Query.Include(p => p.Country)
            .SingleOrDefaultAsync(p => p.PersonID == id, ct);
    }

    public async Task<List<Person>> GetAllPeople(int lastId, int pageSize, CancellationToken ct)
    {
        var query = _context.People.AsNoTracking()
              .Where(p => p.PersonID > lastId)
              .OrderBy(p => p.PersonID)
              .Take(pageSize);

        return await query.ToListAsync(ct);
    }

    public async Task<List<Person>> GetAllPeopleFiltered(PeopleFilterParameters filters, CancellationToken ct)
    {
        var query = _context.People.AsNoTracking();

        if (filters.LastId > 0)
            query = query.Where(p => p.PersonID > filters.LastId);

        if (filters.PersonId.HasValue)
            query = query.Where(p => p.PersonID == filters.PersonId.Value);

        if (!string.IsNullOrWhiteSpace(filters.NationalNumber))
            query = query.Where(p => p.NationalNumber.StartsWith(filters.NationalNumber));

        if (!string.IsNullOrWhiteSpace(filters.FirstName))
            query = query.Where(p => p.FirstName.StartsWith(filters.FirstName));

        if (!string.IsNullOrWhiteSpace(filters.SecondName))
            query = query.Where(p => p.SecondName.StartsWith(filters.SecondName));

        if (!string.IsNullOrWhiteSpace(filters.ThirdName))
            query = query.Where(p => p.ThirdName.StartsWith(filters.ThirdName));

        if (!string.IsNullOrWhiteSpace(filters.LastName))
            query = query.Where(p => p.LastName.StartsWith(filters.LastName));

        query = query.OrderBy(p => p.PersonID)
             .Take(filters.PageSize);

        return await query.ToListAsync(ct);
    }

    public void AddPerson(Person person)
    => _context.People.Add(person);

    public void UpdatePerson(Person person)
    => _context.People.Update(person);

    public Task<bool> IsNationalNumberExist(string nationalNumber, CancellationToken ct)
        => _context.People.AnyAsync(p => p.NationalNumber == nationalNumber, ct);

    public async Task<bool> IsPhoneNumberExist(string? phoneNumber, CancellationToken ct)
    {
        if (phoneNumber is null)
            return false;

        return await _context.People.AnyAsync(p => p.PhoneNumber == phoneNumber, ct);
    }

    public Task<bool> IsNationalNumberReserved(int personID, string nationalNumber, CancellationToken ct)
        => _context.People.AnyAsync(p => p.NationalNumber == nationalNumber && p.PersonID != personID, ct);

    public async Task<bool> IsPhoneNumberReserved(int personID, string? phoneNumber, CancellationToken ct)
    {
        if (phoneNumber is null)
            return false;

        return await _context.People.AnyAsync(p => p.PhoneNumber == phoneNumber && p.PersonID != personID, ct);
    }

    public Task<bool> IsPersonExists(int personId)
    => _context.People.AnyAsync(p => p.PersonID == personId);

    public Task<int> Delete(int personId, CancellationToken ct)
    => _context.People.Where(p => p.PersonID == personId).ExecuteDeleteAsync(ct);

    /// <remarks>
    /// In this operation you MUST call UnitOfWork.SaveChangesAsync() afterwards to persist changes.
    /// </remarks>
    public void StagedDelete(Person person)
    => _context.People.Remove(person);

    public Task<string?> GetImagePath(int personId, CancellationToken ct)
    => _context.People.Where(p => p.PersonID == personId).Select(p => p.ImagePath).SingleOrDefaultAsync(ct);
}