//using SMS.Business.Util;
//using SMS.DataAccess;
//using SMS.Core;
//using System.Net.Http.Headers;
//using System.Security.Cryptography;
//using System.Text;

//namespace SMS.Business.UserServices
//{
//    public class UserService
//    {
//        UserData _userData = new UserData();

//        public async Task<Result<UserDTO>> GetUser(int UserID)
//        {
//            if (UserID <= 0) return Result<UserDTO>.Failure("Invalid user ID.");

//            User user = await _userData.GetUserByID(UserID);
//            if (user == null) return Result<UserDTO>.Failure("User not exist in system.");
            
//            UserDTO userDTO = UserDTO.ToDTO(user);

//            return Result<UserDTO>.Success(userDTO);
//        }

//        public async Task<Result<int>> CreateNewUser(CreateUserDTO createUserDTO)
//        {
//            string Salt = CryptographyHelper._GenerateSalt();
//            string hashedPassword = CryptographyHelper._ComputeHash(createUserDTO.Password, Salt);

//            Result<User> UserResult = createUserDTO.ToEntity(hashedPassword, Salt);
//            if (!UserResult.IsSuccess) return Result<int>.Failure(UserResult.Errors);

//            User user = UserResult.Data;

//            Result ValidatingResult = await UserValidator.ValidatingForAdd(user);
//            if (!ValidatingResult.IsSuccess) return Result<int>.Failure(ValidatingResult.Errors);

//            int UserID = await _userData.CreateUser(user);

//            if (UserID <= 0)
//            {
//                return Result<int>.Failure("Failed to create new user.");
//            }

//            return Result<int>.Success(UserID);
//        }

//        public async Task<Result<bool>> UpdateUser(UpdateUserDTO updateUserDTO)
//        {
//            User user = await _userData.GetUserByID(updateUserDTO.UserID);
//            if (user == null) return Result<bool>.Failure($"User with username {updateUserDTO.Username} not exist in system.");

//            updateUserDTO.UpdateEntity(user);

//            Result ValidatingResult = await UserValidator.ValidatingForUpdate(user);
//            if (!ValidatingResult.IsSuccess)
//                return Result<bool>.Failure(ValidatingResult.Errors);

//            return await _UpdateUser(user);
//        }

//        public async Task<Result<bool>> ChangeUserPassword(ChangeUserPasswordDTO changeUserPasswordDTO)
//        {
//            User user = await _userData.GetUserByID(changeUserPasswordDTO.UserID);
//            if (user == null) 
//                return Result<bool>.Failure($"User with ID {changeUserPasswordDTO.UserID} not exist in system.");

//            Result ValidationResult = changeUserPasswordDTO.Validation();
//            if (!ValidationResult.IsSuccess) 
//                return Result<bool>.Failure(ValidationResult.Errors);

//            string newSalt = CryptographyHelper._GenerateSalt();
//            string newHashedPassword = CryptographyHelper._ComputeHash(changeUserPasswordDTO.NewPassword, newSalt);

//            user.PasswordSalt = newSalt;
//            user.PasswordHash = newHashedPassword;

//            Result ValidatingResult = UserValidator.ValidatingForChangePassword(user, changeUserPasswordDTO.LastPassword, changeUserPasswordDTO.NewPassword);
//            if (!ValidatingResult.IsSuccess)
//                return Result<bool>.Failure(ValidatingResult.Errors);

//            return await _UpdateUser(user);
//        }

//        private async Task<Result<bool>> _UpdateUser(User user)
//        {
//            if (!await _userData.UpdateUser(user))
//            {
//                return Result<bool>.Failure("Failed to update user.");
//            }

//            return Result<bool>.Success(true);
//        }
        
//        public Task<bool> DeactivateUser(int UserID)
//        {
//            return _userData.DeactivateUser(UserID);
//        }

//        public Task<bool> ActiveUser(int UserID)
//        {
//            return _userData.ActivateUser(UserID);
//        }

//    }
//}

