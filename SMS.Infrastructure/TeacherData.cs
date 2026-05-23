
//using Microsoft.Data.SqlClient;
//using SMS.DTOs;
//using System.Data;

//namespace SMS.DataAccess
//{
//    public class TeacherData
//    {
//        private Teacher FromDataReader(IDataReader reader)
//        {
//            int ExitDateOrdinal = reader.GetOrdinal("ExitDate");

//            return new Teacher
//            (
//                teacherID: reader.GetInt32(reader.GetOrdinal("TeacherID")),
//                qualification: reader.GetString(reader.GetOrdinal("Qualification")),
//                startDate: reader.GetDateTime(reader.GetOrdinal("StartDate")),

//                // التحقق من القيمة الفارغة ضروري قبل استدعاء GetDateTime
//                exitDate: reader.IsDBNull(ExitDateOrdinal)
//                          ? (DateTime?)null
//                          : reader.GetDateTime(ExitDateOrdinal),

//                teacherStatus: reader.GetByte(reader.GetOrdinal("TeacherStatus")),
//                createdByUserID: reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
//            );
//        }

//        private void _AddParameters(SqlCommand cmd, Teacher teacher, bool isUpdate = false)
//        {    
//            if (isUpdate == true)
//            {
//                cmd.Parameters.Add("TeacherID", SqlDbType.Int).Value = teacher.TeacherID;
//            }

//            cmd.Parameters.Add("@Qualification", SqlDbType.NVarChar, 50).Value = teacher.Qualification;
//            cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = teacher.StartDate;

//            if (teacher.ExitDate.HasValue)
//                cmd.Parameters.Add("@ExitDate", SqlDbType.Date).Value = teacher.ExitDate.Value;
//            else
//                cmd.Parameters.Add("@ExitDate", SqlDbType.Date).Value = DBNull.Value;

//            cmd.Parameters.Add("@TeacherStatus", SqlDbType.TinyInt).Value = (byte)teacher.TeacherStatus;
//            cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = teacher.CreatedByUserID;
//        }

//        public async Task<int> AddNewTeacher(Teacher teacher)
//        {

//            try
//            {
//                 object obj = await ADO_Helper.ExecuteScalarAsync("sp_Teachers_AddNewTeacher",
//                                         cmd =>  _AddParameters(cmd, teacher),
//                                                  CommandType.StoredProcedure);

//                if (obj != null && int.TryParse(obj.ToString(), out int TeacherID))
//                {
//                    return TeacherID;
//                }

//                return -1;
//            }

//            catch (Exception ex) 
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public async Task<bool> UpdateTeacher(Teacher teacher)
//        {
//            try
//            {
//                return await ADO_Helper.ExecuteNonQueryAsync("sp_Teachers_UpdateTeacher",
//                                           cmd => _AddParameters(cmd, teacher, true),
//                                                    CommandType.StoredProcedure) > 0;
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public async Task<Teacher> GetTeacher(int TeacherID)
//        {
//            try
//            {
//                return await ADO_Helper.ExecuteReaderAsync("sp_Teachers_GetTeacher", 
//                     cmd => cmd.Parameters.AddWithValue(@"TeacherID", TeacherID)
//                                          ,FromDataReader, CommandType.StoredProcedure);
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public async Task<bool> DeleteTeacher(int TeacherID)
//        {
//            try
//            {
//                return await ADO_Helper.ExecuteNonQueryAsync("sp_Teachers_DeleteTeacher",
//                                   cmd => cmd.Parameters.AddWithValue("@TeacherID", TeacherID),
//                                            CommandType.StoredProcedure) > 0;
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public async Task<bool> IsTeacherExist(int TeacherID)
//        {
//            try
//            {
//                return await ADO_Helper.ExecuteScalarAsync("sp_Teachers_IsTeacherExist",
//                                cmd => cmd.Parameters.Add("@TeacherID", SqlDbType.Int).Value = TeacherID,
//                                           CommandType.StoredProcedure) != null;
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public async Task<DataTable> GetAllTeachers()
//        {
//            try
//            {
//                return await ADO_Helper.DataTable_ExecuteReaderAsync("sp_Teachers_GetAllTeachers",
//                                                                        CommandType.StoredProcedure);
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }
//    }
//}
