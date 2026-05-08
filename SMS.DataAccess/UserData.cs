//using Microsoft.Data.SqlClient;
//using SMS.DTOs;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Text;

//namespace SMS.DataAccess
//{
//    public class UserData
//    {
//        private User FromDataReader(IDataReader reader)
//        {
//            int permissionsOrdinal = reader.GetOrdinal("Permissions");

//            return new User
//            (
//                userID: reader.GetInt32(reader.GetOrdinal("UserID")),
//                personID: reader.GetInt32(reader.GetOrdinal("PersonID")),
//                username: reader.GetString(reader.GetOrdinal("Username")),
//                passwordHash: reader.GetString(reader.GetOrdinal("PasswordHash")),
//                passwordSalt: reader.GetString(reader.GetOrdinal("PasswordSalt")),
//                isActive: reader.GetBoolean(reader.GetOrdinal("IsActive")),
//                permissions: reader.GetInt16(permissionsOrdinal),
//                createdByUserID: reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
//            );
//        }

//        private void _AddParameters(SqlCommand command, User user)
//        {
//            // إضافة المعرف فقط في حالة التعديل
//            if (user.UserID != -1)
//            {
//                command.Parameters.Add("@UserID", SqlDbType.Int).Value = user.UserID;
//            }

//            command.Parameters.Add("@PersonID", SqlDbType.Int).Value = user.PersonID;
//            command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = user.Username;

//            // استخدام varchar و nvarchar حسب السكريبت (64 للهاش و 32 للسولت)
//            command.Parameters.Add("@PasswordHash", SqlDbType.VarChar, 64).Value = user.PasswordHash;
//            command.Parameters.Add("@PasswordSalt", SqlDbType.NVarChar, 32).Value = user.PasswordSalt;

//            command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = user.IsActive;

//            command.Parameters.Add("@Permissions", SqlDbType.SmallInt).Value = user.Permissions;                

//            command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = user.CreatedByUserID;
//        }

//        public async Task<User> GetUserByID(int UserID)
//        {
//            try
//            {
//                return await ADO_Helper.ExecuteReaderAsync("sp_Users_GetUserByID",
//                                            cmd => cmd.Parameters.AddWithValue("@UserID", UserID),
//                                            FromDataReader,
//                                            CommandType.StoredProcedure);
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public async Task<int> CreateUser(User user)
//        {
//            try
//            {
//                object obj = await ADO_Helper.ExecuteScalarAsync("sp_Users_AddNewUser",
//                                            cmd =>_AddParameters(cmd, user),
//                                            CommandType.StoredProcedure);

//                if (obj != null && int.TryParse(obj.ToString(), out int UserID))
//                {
//                    return UserID;
//                }

//                return -1;
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public async Task<bool> UpdateUser(User user)
//        {
//            try
//            {
//                return await ADO_Helper.ExecuteNonQueryAsync("sp_Users_UpdateUser",
//                                         cmd => _AddParameters(cmd,user),
//                                            CommandType.StoredProcedure) > 0;
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public async Task<bool> IsUsernameReserved(int UserID, string Username)
//        {
//            try
//            {
//                return await ADO_Helper.ExecuteScalarAsync("sp_Users_IsUsernameReserved",
//                                           cmd =>
//                                           {
//                                               cmd.Parameters.AddWithValue("@UserID", UserID);
//                                               cmd.Parameters.AddWithValue("@Username", Username);
//                                           },
//                                           CommandType.StoredProcedure) != null;

//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public async Task<bool> IsUsernameExist(string Username)
//        {
//            try
//            {
//                return await ADO_Helper.ExecuteScalarAsync("sp_Users_IsUsernameExist",
//                                            cmd => cmd.Parameters.AddWithValue("@Username", Username),
//                                            CommandType.StoredProcedure) != null;
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public async Task<bool> DeactivateUser(int UserID)
//        {
//            try
//            {
//                return await ADO_Helper.ExecuteNonQueryAsync("sp_Users_DeactivateUser",
//                                            cmd => cmd.Parameters.AddWithValue("@UserID", UserID),
//                                            CommandType.StoredProcedure) > 0;
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public async Task<bool> ActivateUser(int UserID)
//        {
//            try
//            {
//                return await ADO_Helper.ExecuteNonQueryAsync("sp_Users_ActivateUser",
//                                            cmd => cmd.Parameters.AddWithValue("@UserID", UserID),
//                                            CommandType.StoredProcedure) > 0;
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }
//    }
//}
