//using DataHelper;
//using Microsoft.Data.SqlClient;
//using SMS.Core;
//using System.Data;

//namespace SMS.DataAccess
//{
//    public class StudentData
//    {
//        private Student FromDataReader(SqlDataReader reader)
//        {
//            int graduationDateOrdinal = reader.GetOrdinal("GraduationDate");

//            return new Student
//            {
//                StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
//                CurrentClassID = reader.GetInt32(reader.GetOrdinal("CurrentClassID")),
//                EnrollmentDate = reader.GetDateTime(reader.GetOrdinal("EnrollmentDate")),
//                StudentStatus = (Student.enStudentStatus)reader.GetByte(reader.GetOrdinal("StudentStatus")),
//                GraduationDate = reader.IsDBNull(graduationDateOrdinal) ? null : reader.GetDateTime(graduationDateOrdinal),
//                CreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
//            };
//        }

//        private static void _AddParameters(SqlCommand command, Student student)
//        {
//            if (student.StudentID > 0)
//            {
//                command.Parameters.Add("@StudentID", SqlDbType.Int).Value = student.StudentID;
//            }

//            command.Parameters.Add("@CurrentClassID", SqlDbType.Int).Value = student.CurrentClassID;
//            command.Parameters.Add("@EnrollmentDate", SqlDbType.Date).Value = student.EnrollmentDate;
//            command.Parameters.Add("@StudentStatus", SqlDbType.TinyInt).Value = (byte)student.StudentStatus;

//            command.Parameters.Add("@GraduationDate", SqlDbType.Date).Value =
//                (object)student.GraduationDate ?? DBNull.Value;

//            command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = student.CreatedByUserID;
//        }

//        public async Task<int> AddNewStudent(Student student)
//        {
//            try
//            {
//                object result = await ADO_Helper.ExecuteScalarAsync(
//                    "sp_Students_AddNewStudent",
//                    cmd => _AddParameters(cmd, student),
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

//        public async Task<bool> UpdateStudent(Student student)
//        {
//            try
//            {
//                int rowsAffected = await ADO_Helper.ExecuteNonQueryAsync(
//                    "sp_Students_UpdateStudent",
//                    cmd => _AddParameters(cmd, student),
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

//        public async Task<bool> DeleteStudent(int studentID)
//        {
//            try
//            {
//                int rowsAffected = await ADO_Helper.ExecuteNonQueryAsync(
//                    "sp_Students_DeleteStudent",
//                    cmd => cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentID,
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

//        public async Task<Student> GetStudentByID(int studentID)
//        {
//            try
//            {
//                return await ADO_Helper.ExecuteReaderAsync(
//                    "sp_Students_GetStudentByID",
//                    cmd => cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentID,
//                     FromDataReader,
//                    CommandType.StoredProcedure
//                );
//            }
//            catch (Exception ex)
//            {
//                DataAccessSettings.LogError(ex);
//                throw;
//            }
//        }

//        public async Task<bool> IsStudentExist(int studentID)
//        {
//            try
//            {
//                object result = await ADO_Helper.ExecuteScalarAsync(
//                    "sp_Students_IsStudentExist",
//                    cmd => cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentID,
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
