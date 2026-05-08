//using SMS.Business.PersonServices;
//using SMS.Business.Util;
//using SMS.DataAccess;
//using SMS.Core;
//using System.Threading.Tasks;

//namespace SMS.Business.TeacherServices
//{
//    internal class TeacherValidator
//    {
//        private static readonly PersonRepository _personData = new PersonRepository();
//        private static readonly TeacherData _teacherData = new TeacherData();

//        private static async Task<Result> GeneralValidating(Teacher teacher)
//        {
//            Result Result = ValidationHelper.IsValid(teacher);
//            if (!Result.IsSuccess) return Result;

//            if (teacher.StartDate > DateTime.Now)
//                return Result.Failure("Start Date cannot be in the future.");

//            return Result.Success();
//        }

//        public static async Task<Result> ValidateForAdd(Teacher teacher)
//        {
//            Result ValidatingResult = await GeneralValidating(teacher);
//            if (!ValidatingResult.IsSuccess) return ValidatingResult;

//            if (await _teacherData.IsTeacherExist(teacher.TeacherID))
//                Result.Failure("This teacher is already exist");

//            Person person = await _personData.GetPersonByID(teacher.TeacherID);
            
//            if (person == null) 
//                Result.Failure($"No person found with ID {teacher.TeacherID} to be a teacher",ErrorCode.PersonNotFound);

//            if (string.IsNullOrEmpty(person.PhoneNumber)) 
//                Result.Failure("A Person must have a phone number to be a teacher");

//            return Result.Success();
//        }

//        public static async Task<Result> ValidateForUpdate(Teacher teacher)
//        {
//            Result ValidatingResult = await GeneralValidating(teacher);
//            if (!ValidatingResult.IsSuccess) return ValidatingResult;

//            if (teacher.ExitDate < teacher.StartDate)
//                return Result.Failure("can not be Exit date before start date",ErrorCode.IncorrectDate);

//            //Task<bool> IsTeacherExist = _teacherData.IsTeacherExist(teacher.TeacherID);
//            //if (!await IsTeacherExist)  return ServiceResult.Failure($"Teacher with ID {teacher.TeacherID} does not exist.");
            
//            return Result.Success();
//        }
//    }
//}
