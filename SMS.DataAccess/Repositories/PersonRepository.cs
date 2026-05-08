using Microsoft.EntityFrameworkCore;
using SMS.Core.Entities;
using System.Numerics;

namespace SMS.DataAccess
{
    public class PersonRepository
    {
        private readonly AppDbContext _context;
        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<Person?> GetPersonByID(int Id, bool Tracking = false)
        {
            var Query = _context.People.AsQueryable();
            
            if (!Tracking) Query = Query.AsNoTracking();

            return Query.Include(p => p.Country)
                .SingleOrDefaultAsync(p => p.PersonID == Id);
        }
         
        public async Task<int> AddPerson(Person person)
        {
            _context.People.Add(person);

            await _context.SaveChangesAsync();

            return person.PersonID;
        }

        public async Task<bool> UpdatePerson(Person person)
        {
            _context.People.Update(person);

            if (await _context.SaveChangesAsync() > 0)
                return true;
            
            return false;
        }

        public Task<bool> IsNationalNumberExist(string nationalNumber)
            => _context.People.AnyAsync(p => p.NationalNumber == nationalNumber);

        public async Task<bool> IsPhoneNumberExist(string? phoneNumber)
        {
            if (phoneNumber is null)
                return false;

            return await _context.People.AnyAsync(p => p.PhoneNumber == phoneNumber);
        }
            


        public Task<bool> IsNationalNumberReserved(int personID, string nationalNumber)
            => _context.People.AnyAsync(p => p.NationalNumber == nationalNumber &&  p.PersonID != personID);


        //  => _dapperHelper.IsPositivResult("sp_People_IsNationalNumberReserved", new { PersonID = personID, NationalNumber = nationalNumber });

        //public Task<bool> DeletePerson(int id)
        //    => _dapperHelper.Delete("sp_People_Delete", id);

        //public Task<bool> IsPersonExists(int id)
        //    => _dapperHelper.IsExist("sp_People_IsExist", id);



        //public Task<bool> HasPhoneNumber(int personID)
        //  => _dapperHelper.IsPositivResult("sp_People_HasPhoneNumber", new { PersonID = personID });
    }
}
