using SMS.Business.Util;
using SMS.DataAccess;
using System.Text.RegularExpressions;
using SMS.Core.Entities;
using SMS.Business.Util.ErrorCodes;

namespace SMS.Business.PersonServices
{
    public class PersonValidator
    {
        private readonly PersonRepository _personRepository;
        public PersonValidator(PersonRepository personRepository) { _personRepository = personRepository; }

        private static Result _ValidateFieldFormats(Person person)
        {

            //if (!Regex.IsMatch(person.FirstName, @"^[\p{L}\s]+$") || !Regex.IsMatch(person.SecondName, @"^[\p{L}\s]+$") ||
            //    !Regex.IsMatch(person.ThirdName, @"^[\p{L}\s]+$") || !Regex.IsMatch(person.LastName, @"^[\p{L}\s]+$"))
            //{
            //    errors.Add("Please enter a valid name without using numbers or symbols.");
            //}

            //if (!string.IsNullOrEmpty(person.PhoneNumber))
            //{
            //    if (person.PhoneNumber.Length < 10)
            //    {
            //        errors.Add("Phone number must be at least 10 characters long.");
            //    }
            //}

            return Result.Success();
        }

        public async Task<Result> ValidationForAdd(Person person)
        {
            var result = new Result(BaseErrorCode.AlreadyExists);

            var IsNationalNumberExist = await _personRepository.IsNationalNumberExist(person.NationalNumber);
            var IsPhoneNumberExist = await _personRepository.IsPhoneNumberExist(person.PhoneNumber);

            if (IsNationalNumberExist)
                result.AddError("NationalNumber already exists", ErrorCode.NationalNumberAlreadyExists, nameof(person.NationalNumber));

            if (IsPhoneNumberExist)
                result.AddError("Phone number already exists", ErrorCode.PhoneNumberAlreadyExists, nameof(person.PhoneNumber));

            return result;
        }

        public async Task<Result> ValidationForUpdate(Person person)
        {
            var result = new Result(BaseErrorCode.AlreadyExists);

            var IsNationalNumberExist = await _personRepository.IsNationalNumberExist(person.NationalNumber);
            var IsPhoneNumberExist = await _personRepository.IsPhoneNumberExist(person.PhoneNumber);

            if (IsNationalNumberExist)
                result.AddError("NationalNumber is already exist in system", ErrorCode.NationalNumberReserved);

            if (IsPhoneNumberExist)
                result.AddError("Phone number is already exist in system", ErrorCode.PhoneNumberNumberReserved);

            return result;
        }
    }
}
