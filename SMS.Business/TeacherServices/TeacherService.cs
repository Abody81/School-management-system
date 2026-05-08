//using SMS.Business.Util;
//using SMS.DataAccess;
//using SMS.Core;
//using System.Data;

//namespace SMS.Business.TeacherServices
//{
//    public class TeacherService
//    {
//        private readonly TeacherData _teacherData = new TeacherData();

//        public async Task<Result<int>> AddNewTeacher(CreateTeacherDTO createTeacherDTO)
//        {
//            try
//            {
//                Teacher teacher = createTeacherDTO.ToEntity();

//                Result ValidatingResult = await TeacherValidator.ValidateForAdd(teacher);
//                if (!ValidatingResult.IsSuccess) return Result<int>.Failure(ValidatingResult.Errors, ValidatingResult.AppErrorCode);

//                int TeacherID = await _teacherData.AddNewTeacher(teacher);

//                if (TeacherID > 0)
//                {
//                    return Result<int>.Success(TeacherID);
//                }

//                return Result<int>.Failure("An unknown database error occurred", ErrorCode.DatabaseFailure);
//            }
//            catch (Exception ex)
//            {
//                Logger.LogError(ex);

//                return Result<int>.Failure($"An error occurred while adding new teacher",ErrorCode.InternalError);
//            }
//        }

//        public async Task<Result<bool>> UpdateTeacher(UpdateTeacherDTO updateTeacherDTO)
//        {
//            try
//            {
//                Teacher teacher = await _teacherData.GetTeacher(updateTeacherDTO.TeacherID);
//                if (teacher == null) return Result<bool>.Failure("you can't update teacher data because teacher not exist in system.", ErrorCode.TeacherNotFound);

//                updateTeacherDTO.UpdateEntity(teacher);

//                Result ValidatingResult = await TeacherValidator.ValidateForUpdate(teacher);
//                if (!ValidatingResult.IsSuccess) return Result<bool>.Failure(ValidatingResult.Errors, ValidatingResult.AppErrorCode);

//                bool isUpdated = await _teacherData.UpdateTeacher(teacher);

//                if (isUpdated)
//                    return Result<bool>.Success(true);

//                return Result<bool>.Failure("An unknown database error occurred", ErrorCode.DatabaseFailure);
//            }
//            catch (Exception ex)
//            {
//                Logger.LogError(ex);

//                return Result<bool>.Failure($"An error occurred while updating teacher data", ErrorCode.InternalError);
//            }
//        }

//        public async Task<Result<Teacher>> GetTeacher(int teacherID)
//        {
//            try
//            {
//                if (teacherID <= 0) return Result<Teacher>.Failure("Invalid teacher ID.", ErrorCode.InvalidTeacherId);
//                Teacher teacher = await _teacherData.GetTeacher(teacherID);

//                if (teacher == null) return Result<Teacher>.Failure("Teacher not found in system.", ErrorCode.TeacherNotFound);

//                return Result<Teacher>.Success(teacher);
//            }

//            catch (Exception ex)
//            {
//                Logger.LogError(ex);

//                return Result<Teacher>.Failure($"An error occurred while retrieving teacher data", ErrorCode.InternalError);
//            }
//        }

//        public async Task<Result<bool>> DeleteTeacher(int TeacherID)
//        {
//            try
//            {
//                if (TeacherID <= 0)
//                    return Result<bool>.Failure("Invalid teacher ID.", ErrorCode.InvalidTeacherId);

//                bool isDeleted = await _teacherData.DeleteTeacher(TeacherID);

//                if (isDeleted)
//                    return Result<bool>.Success(true);

//                return Result<bool>.Failure("An unknown database error occurred", ErrorCode.DatabaseFailure);
//            }

//            catch(Exception ex)
//            {
//                Logger.LogError(ex);

//                return Result<bool>.Failure($"An error occurred while deleting teacher data", ErrorCode.InternalError);
//            }
//        }

//        public Task<DataTable> GetAllTeachers()
//        {
//            return _teacherData.GetAllTeachers();
//        }
//    }
//}
