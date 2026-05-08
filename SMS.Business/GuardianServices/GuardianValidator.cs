//using SMS.Business.PersonServices;
//using SMS.Business.Util;
//using SMS.Core;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace SMS.Business.GuardianServices
//{
//    internal class GuardianValidator
//    {
//        PersonService _personServices = new PersonService();

//        private static Result _ValidateFieldFormats(Guardian guardian)
//        {
//            if (Regex.IsMatch(guardian.JobTitle, @"^[\p{L}\s]+$"))
//            {
//                return Result.Failure("Please enter a valid job title without using numbers or symbols.");
//            }

//            return Result.Success();
//        }

//        private static Result _Validating(Guardian guardian)
//        {
//            Result result = null;
            
//            result = ValidationHelper.IsValid(guardian);
//            if (!result.IsSuccess) return Result.Failure(result.Errors);
            
//            result = _ValidateFieldFormats(guardian);
//            if (!result.IsSuccess) return Result.Failure(result.Errors);

//            return Result.Success();
//        }

//        public async Task<Result> ValidationForAdd(Guardian guardian)
//        {
//            Result ValidatingResult = _Validating(guardian);
//            if (!ValidatingResult.IsSuccess) return Result.Failure(ValidatingResult.Errors);

//            Result<Person> PersonResult = await _personServices.GetPerson(guardian.GuardianID);
//            if (!PersonResult.IsSuccess) return Result.Failure(PersonResult.Errors);

//            Person person = PersonResult.Data;

//            if (person.PhoneNumber == null)
//                return Result.Failure("Person must has a phone number to be a guardian");

//            return Result.Success();
//        }

//        public Result ValidationForUpdate(Guardian guardian)
//        {
//            return _Validating(guardian);
//        }
//    }
//}
