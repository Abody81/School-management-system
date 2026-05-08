//using SMS.Business.Util;
//using SMS.DataAccess;
//using SMS.Core;

//namespace SMS.Business.GuardianServices
//{
//    public class GuardianService
//    {
//        GuardianData _guardianData = new GuardianData();
//        GuardianValidator _guardianValidator = new GuardianValidator();

//        public async Task<Result<Guardian>> GetGuardianInfo(int GuardianID)
//        {
//            if (GuardianID <= 0)
//                return Result<Guardian>.Failure("Invalid Guardian ID");

//            Guardian guardian = await _guardianData.GetGuardianByID(GuardianID);

//            if (guardian == null)
//                return Result<Guardian>.Failure("Guardian not exist in system");

//            return Result<Guardian>.Success(guardian);
//        }

//        public async Task<Result<int>> AddNewGuardian(Guardian guardian)
//        {
//            if (guardian == null)
//                return Result<int>.Failure("Guardian data is empty");

//            Result ValidationResult = await _guardianValidator.ValidationForAdd(guardian);
//            if (!ValidationResult.IsSuccess) return Result<int>.Failure(ValidationResult.Errors);

//            int newGuardianID = await _guardianData.AddNewGuardian(guardian);

//            if (newGuardianID <= 0)
//                return Result<int>.Failure("Failed to add new guardian");

//            return Result<int>.Success(newGuardianID);
//        }

//        public async Task<Result> UpdateGuardianInfo(Guardian guardian)
//        {
//            if (guardian == null)
//                return Result.Failure("Guardian data is empty");

//            Result ValidationResult = _guardianValidator.ValidationForUpdate(guardian);
//            if (!ValidationResult.IsSuccess) return Result.Failure(ValidationResult.Errors);

//            bool isUpdated = await _guardianData.UpdateGuardianInfo(guardian);
            
//            if (!isUpdated)
//                return Result.Failure("Failed to update guardian info");

//            return Result.Success();
//        }

//        public async Task<Result> DeleteGuardian(int GuardianID)
//        {
//            if (GuardianID <= 0)
//                return Result.Failure("Invalid Guardian ID");

//            bool isDeleted = await _guardianData.DeleteGuardian(GuardianID);
            
//            if (!isDeleted)
//                return Result.Failure("Failed to delete guardian");

//            return Result.Success();
//        }


//    }
//}
