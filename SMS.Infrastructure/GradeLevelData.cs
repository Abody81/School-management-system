//using Microsoft.Data.SqlClient;
//using SMS.DTOs;
//using System.Data;

//namespace SMS.DataAccess
//{
//    public class GradeLevelData
//    {
//        private static GradeLevel FromDataReader(SqlDataReader reader)
//        {
//            return new GradeLevel
//            {
//                GradeLevelID = reader.GetInt32(reader.GetOrdinal("GradeLevelID")),
//                GradeLevelName = reader.GetString(reader.GetOrdinal("GradeLevel")),
//                StageID = reader.GetInt32(reader.GetOrdinal("StageID")),
//                TrackID = reader.GetInt32(reader.GetOrdinal("TrackID")),
//                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
//            };
//        }

//        private static void _AddParameters(SqlCommand command, GradeLevel gradeLevel)
//        {
//            // ID is only used for Update/Delete, not Insert (Identity)
//            if (gradeLevel.GradeLevelID > 0)
//            {
//                command.Parameters.Add("@GradeLevelID", SqlDbType.Int).Value = gradeLevel.GradeLevelID;
//            }

//            command.Parameters.Add("@GradeLevel", SqlDbType.NVarChar, 10).Value = gradeLevel.GradeLevelName;
//            command.Parameters.Add("@StageID", SqlDbType.Int).Value = gradeLevel.StageID;
//            command.Parameters.Add("@TrackID", SqlDbType.Int).Value = gradeLevel.TrackID;
//            command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = gradeLevel.IsActive;
//        }

//        public async Task<GradeLevel> GetGradeLevel(int gradeLevelID)
//        {
//            try
//            {
//                return await ADO_Helper.ExecuteReaderAsync("sp_GradeLevel_GetByID",
//                    cmd => cmd.Parameters.AddWithValue("@GradeLevelID", gradeLevelID),
//                    FromDataReader,
//                    CommandType.StoredProcedure);
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public async Task<bool> IsGradeLevelExist(int gradeLevelID)
//        {
//            try
//            {
//                return await ADO_Helper.ExecuteScalarAsync("sp_GradeLevel_IsGradeLevelExist",
//                    cmd => cmd.Parameters.AddWithValue("@GradeLevelID", gradeLevelID),
//                    CommandType.StoredProcedure) != null;
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public async Task<int> AddNewGradeLevel(GradeLevel gradeLevel)
//        {
//            try
//            {
//                object obj = await ADO_Helper.ExecuteScalarAsync("sp_GradeLevel_AddNewGradeLevel",
//                    cmd => _AddParameters(cmd, gradeLevel),
//                    CommandType.StoredProcedure);

//                return (obj != null && int.TryParse(obj.ToString(), out int insertedID)) ? insertedID : -1;
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public async Task<bool> UpdateGradeLevel(GradeLevel gradeLevel)
//        {
//            try
//            {
//                return await ADO_Helper.ExecuteNonQueryAsync("sp_GradeLevel_Update",
//                    cmd => _AddParameters(cmd, gradeLevel),
//                    CommandType.StoredProcedure) > 0;
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public async Task<bool> DeleteGradeLevel(int gradeLevelID)
//        {
//            try
//            {
//                return await ADO_Helper.ExecuteNonQueryAsync("sp_GradeLevel_DeleteGradeLevel",
//                    cmd => cmd.Parameters.AddWithValue("@GradeLevelID", gradeLevelID),
//                    CommandType.StoredProcedure) > 0;
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public async Task<bool> DeActivate(int gradeLevelID)
//        {
//            try
//            {
//                return await ADO_Helper.ExecuteNonQueryAsync("sp_GradeLevel_DeActivate",
//                    cmd => cmd.Parameters.AddWithValue("@GradeLevelID", gradeLevelID),
//                    CommandType.StoredProcedure) > 0;
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }
//    }
//}
