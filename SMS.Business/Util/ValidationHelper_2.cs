//using System.ComponentModel.DataAnnotations;
//using System.Diagnostics;

//namespace SMS.Business.Util
//{

//    [DebuggerStepThrough]
//    internal class ValidationHelper
//    {
//        public static Result IsValid(object Entity)
//        {
//            var validationResults = new List<ValidationResult>();

//            var validationContext = new ValidationContext(Entity);

//            bool IsValid = Validator.TryValidateObject(Entity, validationContext, validationResults, true);

//            if (IsValid) return Result.Success();

//            var errors = new List<string>();
//            foreach (ValidationResult Result in validationResults)
//            {
//                errors.Add(Result.ErrorMessage);
//            }

//            return Result.Failure(errors, ErrorCode.ValidationError);
//        }
//    }
//}
