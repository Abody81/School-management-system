//using Microsoft.Data.SqlClient;
//using SMS.Core;
//using System.Data;

//namespace SMS.DataAccess
//{
//    public class GuardianData
//    {
//        private static Guardian FromDataReader(IDataReader reader)
//        {
//            int JobTitleOrdinal = reader.GetOrdinal("JobTitle");

//            return new Guardian
//            (
//                guardianID: reader.GetInt32(reader.GetOrdinal("GuardianID")),
//                jobTitle: reader.IsDBNull(JobTitleOrdinal) ? null : reader.GetString(JobTitleOrdinal),
//                createdByUserID: reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
//            );
//        }

//        private static void _AddParameters(SqlCommand command, Guardian guardian)
//        {
//            if (guardian.GuardianID > 0)
//            {
//                command.Parameters.Add("@GuardianID", SqlDbType.Int).Value = guardian.GuardianID;
//            }

//            command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = guardian.CreatedByUserID;
            
//            if (string.IsNullOrEmpty(guardian.JobTitle))
//                command.Parameters.Add("@JobTitle", SqlDbType.NVarChar).Value = DBNull.Value;
//            else
//                command.Parameters.Add("@JobTitle", SqlDbType.NVarChar).Value = guardian.JobTitle;
//        }

//        public  async Task<int> AddNewGuardian(Guardian guardian)
//        {
//            try
//            {
//                object result = await ADO_Helper.ExecuteScalarAsync(
//                    "sp_Guardians_AddNewGuardian",
//                    cmd => _AddParameters(cmd, guardian),
//                    CommandType.StoredProcedure
//                );

//                return (result != null && int.TryParse(result.ToString(), out int id)) ? id : -1;
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public  async Task<bool> UpdateGuardianInfo(Guardian guardian)
//        {
//            try
//            {
//                int rowsAffected = await ADO_Helper.ExecuteNonQueryAsync(
//                    "sp_Guardians_UpdateGuardian",
//                    cmd => _AddParameters(cmd, guardian),
//                    CommandType.StoredProcedure
//                );

//                return rowsAffected > 0;
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public  async Task<bool> DeleteGuardian(int guardianID)
//        {
//            try
//            {
//                int rowsAffected = await ADO_Helper.ExecuteNonQueryAsync(
//                    "sp_Guardians_DeleteGuardian",
//                    cmd => cmd.Parameters.Add("@GuardianID", SqlDbType.Int).Value = guardianID,
//                    CommandType.StoredProcedure
//                );

//                return rowsAffected > 0;
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public  async Task<Guardian> GetGuardianByID(int guardianID)
//        {
//            try
//            {
//                return await ADO_Helper.ExecuteReaderAsync(
//                    "sp_Guardians_GetGuardianByID",
//                    cmd => cmd.Parameters.Add("@GuardianID", SqlDbType.Int).Value = guardianID,
//                    FromDataReader,
//                    CommandType.StoredProcedure
//                );
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public  async Task<bool> IsGuardianExist(int guardianID)
//        {
//            try
//            {
//                object result = await ADO_Helper.ExecuteScalarAsync(
//                    "sp_Guardians_IsGuardianExist",
//                    cmd => cmd.Parameters.Add("@GuardianID", SqlDbType.Int).Value = guardianID,
//                    CommandType.StoredProcedure
//                );

//                return result != null;
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }
//    }
//}