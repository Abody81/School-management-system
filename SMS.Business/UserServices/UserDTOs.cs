//using SMS.Business.Util;
//using SMS.Core;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace SMS.Business.UserServices
//{
//    public class UserDTO
//    {
//        public int UserID { set; get; }
//        public int PersonID { set; get; }
//        public string Username { get; set; }
//        public bool IsActive { get; set; }
//        public short? Permissions { get; set; }
//        public int CreatedByUserID { get; set; }

//        public static UserDTO ToDTO(User user)
//        {
//            return new UserDTO
//            {
//                UserID = user.UserID,
//                PersonID = user.PersonID,
//                Username = user.Username,
//                IsActive = user.IsActive,
//                Permissions = user.Permissions,
//                CreatedByUserID = user.CreatedByUserID
//            };
//        }

//    }

//    public class CreateUserDTO
//    {
//        public int PersonID { get; set; }
//        public string Username { get; set; }
//        public string Password { get; set; }
//        public short Permissions { get; set; }
//        public int CreatedByUserID { get; set; }

//        internal Result<User> ToEntity(string passwordHash, string passwordSalt)
//        {
//            if (string.IsNullOrWhiteSpace(Password) || Password.Length > 64 || Password.Length < 7)
//            {
//                return Result<User>.Failure("Password must be between 8 and 64 characters.");
//            }

//            User user = new User 
//            {
//                PersonID = this.PersonID,
//                Username = this.Username,
//                Permissions = this.Permissions,
//                CreatedByUserID = this.CreatedByUserID,

//                PasswordHash = passwordHash,
//                PasswordSalt = passwordSalt,
//                IsActive = true,
//            };

//            return Result<User>.Success(user);
//        }
//    }

//    public class UpdateUserDTO
//    {
//        public int UserID { get; set; }
//        public string Username { get; set; }
//        public short Permissions { get; set; }
//        public bool IsActive { get; set; }

//        internal User UpdateEntity(User user)
//        { 
//            user.Username = this.Username;
//            user.Permissions = this.Permissions;
//            user.IsActive = this.IsActive;

//            return user;
//        }
//    }

//    public class ChangeUserPasswordDTO
//    {
//        public int UserID { get; set; }
//        public string LastPassword { get; set; }    
//        public string NewPassword { get; set; }

//        internal Result Validation()
//        {
//            if (string.IsNullOrWhiteSpace(NewPassword) || NewPassword.Length > 64 || NewPassword.Length <= 7)
//                return Result.Failure("New password must be between 8 and 64 characters.");

//            return Result.Success();
//        }
//    }

//}

