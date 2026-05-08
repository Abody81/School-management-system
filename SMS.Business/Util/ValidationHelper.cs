
using SMS.Business.Util.ErrorCodes;

namespace SMS.Business.Util
{
    public static class ValidationHelper
    {
        public static Result ValidateID(int Id, int MinId)
        {
            if (Id < MinId)
                return Result.Failure("Invalid ID", ErrorCode.InvalidId);

            return Result.Success();
        }

        public static Result<T> ValidateID<T>(int Id, int MinId)
        {
            if (Id < MinId)
                return Result<T>.Failure($"Invalid {typeof(T).Name} ID", ErrorCode.InvalidId);

            return Result<T>.Success(default);
        }

        public static Result<T> ValidateNotNull<T>(T entity)
        {
            if (entity == null)
                return Result<T>.Failure($" {typeof(T).Name} not found", ErrorCode.NotFound);

            return Result<T>.Success(entity);
        }

        //protected async virtual Task<Result> ValidateIsExist(int id)
        //{
        //    var idValidation = ValidateID(id); if (!idValidation.IsSuccess) return idValidation;

        //    bool exists = await _repository.IsExist(id);

        //    if (!exists)
        //        return Result.Failure($"{typeof(T).Name} with ID {id} does not exist.", AppErrorCode.NotFound);

        //    return Result.Success();
        //}

        //public async virtual Task<Result<List<T>>> GetAll(int pageNumber, int rowsPerPage)
        //{
        //    List<T> entities = await _repository.GetAll(pageNumber, rowsPerPage);

        //    if (entities == null || entities.Count == 0)
        //        return Result<List<T>>.Failure($"No {typeof(T).Name} records found.", AppErrorCode.NotFound);

        //    return Result<List<T>>.Success(entities);
        //}

        //public async virtual Task<Result<T>> GetById(int id)
        //{
        //    var idValidation = ValidateID<T>(id); if (!idValidation.IsSuccess) return idValidation;

        //    T entity = await _repository.GetByID(id);

        //    return ValidateNotNull(entity);
        //}

        //public async virtual Task<Result> Delete(int id)
        //{
        //    var existValidation = await ValidateIsExist(id); if (!existValidation.IsSuccess) return existValidation;

        //    bool Deleted = await _repository.Delete(id);

        //    return Deleted
        //        ? Result.Success()
        //        : Result.Failure($"Failed to delete {typeof(T).Name} with ID {id}.", AppErrorCode.DatabaseFailure);
        //}

        //public async virtual Task<Result<bool>> IsExist(int id)
        //{
        //    var ValiId = ValidateID<bool>(id); if (!ValiId.IsSuccess) return ValiId;

        //    bool exist = await _repository.IsExist(id);

        //    return Result<bool>.Success(exist);
        //}

        //public async Task<Result> Warmup()
        //{
        //    try
        //    {
        //        await _repository.Warmup();
        //        return Result.Success();
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result.Failure("Warmup failed", AppErrorCode.DatabaseFailure);
        //    }
        //}
    }
}