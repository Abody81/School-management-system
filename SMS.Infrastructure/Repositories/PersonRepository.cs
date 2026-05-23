using Microsoft.EntityFrameworkCore;
using SMS.Application.Interfaces.Repositories;
using SMS.Domain.Entities;

namespace SMS.Infrastructure
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;
        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<Person?> GetPersonById(int id, bool tracking = false, CancellationToken ct = default)
        {
            var Query = _context.People.AsQueryable();

            if (!tracking) Query = Query.AsNoTracking();

            return Query.Include(p => p.Country)
                .SingleOrDefaultAsync(p => p.PersonID == id);
        }

        public void AddPerson(Person person)
        => _context.People.Add(person);
        
        public void UpdatePerson(Person person)
        =>  _context.People.Update(person);

        public Task<bool> IsNationalNumberExist(string nationalNumber, CancellationToken ct = default)
            => _context.People.AnyAsync(p => p.NationalNumber == nationalNumber);

        public async Task<bool> IsPhoneNumberExist(string? phoneNumber, CancellationToken ct = default)
        {
            if (phoneNumber is null)
                return false;

            return await _context.People.AnyAsync(p => p.PhoneNumber == phoneNumber);
        }
            
        public Task<bool> IsNationalNumberReserved(int personID, string nationalNumber, CancellationToken ct = default)
            => _context.People.AnyAsync(p => p.NationalNumber == nationalNumber &&  p.PersonID != personID);

        public async Task<bool> IsPhoneNumberReserved(int personID, string? phoneNumber, CancellationToken ct = default)
        {
            if (phoneNumber is null)
                return false;

            return await _context.People.AnyAsync(p => p.PhoneNumber == phoneNumber && p.PersonID != personID);
        }



        //  => _dapperHelper.IsPositivResult("sp_People_IsNationalNumberReserved", new { PersonID = personID, NationalNumber = nationalNumber });

        //public Task<bool> DeletePerson(int id)
        //    => _dapperHelper.Delete("sp_People_Delete", id);

        //public Task<bool> IsPersonExists(int id)
        //    => _dapperHelper.IsExist("sp_People_IsExist", id);



        //public Task<bool> HasPhoneNumber(int personID)
        //  => _dapperHelper.IsPositivResult("sp_People_HasPhoneNumber", new { PersonID = personID });
    }
}
