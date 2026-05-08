//using SMS.Business.Util;
//using SMS.DataAccess;
//using SMS.Core;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace SMS.Business.UserServices
//{
//    internal class UserValidator
//    {
//        private static  UserData _userData = new UserData();

//        private static Result _ValidateFieldFormats(User user)
//        {
//           // List<string> errors = new List<string>();

//            if (!Regex.IsMatch(user.Username, @"^[\p{L}\d]+$"))
//            {
//                Result.Failure("Please enter a valid username without using symbols.");
//            }

//            return Result.Success();
//        }

//        private static Result _Validating(User user)
//        {
//            Result result = null;

//            result = _ValidateFieldFormats(user);
//            if (!result.IsSuccess) return result;

//            result = ValidationHelper.IsValid(user);
//            if (!result.IsSuccess) return result;

//            return Result.Success();
//        }
        
//        public static async Task<Result> ValidatingForAdd(User user)
//        {
//            Result ValidatingResult = _Validating(user);
//            if (!ValidatingResult.IsSuccess) return Result.Failure(ValidatingResult.Errors);

//            bool IsUsernameExist = await _userData.IsUsernameExist(user.Username);
            
//            if (IsUsernameExist)
//                return Result.Failure("The username is already taken by another user.");

//            return Result.Success();
//        }

//        public static async Task<Result> ValidatingForUpdate(User user)
//        {
//            Result ValidatingResult = _Validating(user);
//            if (!ValidatingResult.IsSuccess) return Result.Failure(ValidatingResult.Errors);

//            bool IsUsernameReserved = await _userData.IsUsernameReserved(user.UserID, user.Username);
            
//            if (IsUsernameReserved)
//                return Result.Failure("The username is already taken by another user.");

//            return Result.Success();
//        }

//        public static Result ValidatingForChangePassword(User user, string LastPassword, string NewPassword)
//        {
//            Result result = _Validating(user);
//            if (!result.IsSuccess) return result;

//            string LastHashedPassword = CryptographyHelper._ComputeHash(LastPassword, user.PasswordSalt);

//            if (LastHashedPassword != user.PasswordHash)
//                return Result<bool>.Failure("Last password is incorrect.");

//            if (CryptographyHelper._IsPasswordMatch(NewPassword, user.PasswordHash, user.PasswordSalt))
//                return Result<bool>.Failure("New password must be different from the last password.");

//            return Result.Success();
//        }
//    }
//}
