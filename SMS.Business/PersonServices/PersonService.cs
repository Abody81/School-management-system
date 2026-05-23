using AutoMapper;
using SMS.Business.Util;
using SMS.Business.Util.ErrorCodes;
using SMS.Domain.Entities;
using SMS.Infrastructure;

namespace SMS.Business.PersonServices
{
    public class PersonService
    {
        private readonly PersonRepository _repo;
        private readonly IMapper _mapper;
        private readonly PersonValidator _personValidator;

        public PersonService(PersonRepository personRepository, PersonValidator personValidator, IMapper mapper)
        {
            _repo = personRepository;
            _personValidator = personValidator;
            _mapper = mapper;
        }

        private readonly static int MinId = 1;

        public async Task<Result<Person>> GetPersonByID(int PersonID)
        {
            try
            {
                var ValiId = ValidationHelper.ValidateID(PersonID, MinId);
                if (!ValiId.IsSuccess) return Result<Person>.Failure(ValiId);

                Person? person = await _repo.GetPersonByID(PersonID);

                if (person is null)
                    return Result<Person>.Failure("Person not found", ErrorCode.NotFound);

                return Result<Person>.Success(person);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);

                return Result<Person>.Failure("An internal error occurred while getting the person.", ErrorCode.InternalError);
            }
        }

        public async Task<Result<int>> AddPerson(Person person, Stream imageStream, string fileExtension)
        {
            var ValidateResult = await _personValidator.ValidationForAdd(person);
            if (!ValidateResult.IsSuccess) return Result<int>.Failure(ValidateResult);

            string filePath = await ImageHandler.SaveImageAsync(imageStream, fileExtension);

            if (filePath == null)
                return Result<int>.Failure("An error occurred while processing the image. Please select a valid image.", ErrorCode.UnsupportedMediaType);

            person.ImagePath = filePath;

            try
            {
                int PersonID = await _repo.AddPerson(person);

                if (PersonID > 0)
                {
                    return Result<int>.Success(PersonID);
                }

                else
                {
                    ImageHandler.DeleteImage(person.ImagePath);
                    return Result<int>.Failure("An unknown database error occurred.", ErrorCode.DatabaseFailure);
                }
            }

            catch (Exception ex)
            {
                Logger.LogError(ex);

                ImageHandler.DeleteImage(person.ImagePath);

                return Result<int>.Failure("An internal error occurred while adding the person.", ErrorCode.InternalError);
            }
        }


        public async Task<Result> UpdatePerson(Person person, Stream? imageStream, string? fileExtension)
        {
            try
            {
                Person? OldPerson = await _repo.GetPersonByID(person.PersonID, true);
                if (OldPerson == null)
                    return Result<bool>.Failure($"Person with ID {person.PersonID} not found in system!", ErrorCode.NotFound);

                Result result = await _personValidator.ValidationForUpdate(person);
                if (!result.IsSuccess) return Result<bool>.Failure(result);


                OldPerson = _mapper.Map(person, OldPerson);


                return await _PrepareUpdatePerson(OldPerson, imageStream, fileExtension);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);

                return Result<bool>.Failure("An internal error occurred while updating the person.", ErrorCode.InternalError);
            }
        }

        private async Task<Result> _PrepareUpdatePerson(Person person, Stream? imageStream, string? fileExtension)
        {
            string? newSavedPath = null;
            bool ImageChanged = imageStream != null;

            string? PreviousImagePath = null;

            try
            {
                if (ImageChanged)
                {
                    newSavedPath = await ImageHandler.SaveImageAsync(imageStream!, fileExtension!);

                    if (newSavedPath == null)
                        return Result.Failure("An error occurred while processing the image. Please select a valid image.", ErrorCode.UnsupportedMediaType);

                    PreviousImagePath = person.ImagePath;
                    person.ImagePath = newSavedPath;
                }

                if (await _repo.UpdatePerson(person))
                {
                    if (ImageChanged)
                    {
                        ImageHandler.DeleteImage(PreviousImagePath!);
                    }

                    return Result<bool>.Success(true);
                }

                else
                {
                    if (ImageChanged && !string.IsNullOrEmpty(newSavedPath))
                    {
                        ImageHandler.DeleteImage(newSavedPath!);
                    }

                    return Result<bool>.Failure("Database update failed", ErrorCode.DatabaseFailure);
                }

            }
            catch (Exception ex)
            {
                Logger.LogError(ex);

                if (ImageChanged && !string.IsNullOrEmpty(newSavedPath))
                    ImageHandler.DeleteImage(newSavedPath!);

                return Result<bool>.Failure("An internal error occurred while updating the person.", ErrorCode.InternalError);
            }
        }

        //public async Task<Result> DeletePerson(int PersonID)
        //{
        //    try
        //    {
        //        var PersonResult = await this.GetPersonByID(PersonID);
        //        if (!PersonResult.IsSuccess)
        //            return Result.Failure(PersonResult.Errors, PersonResult.ErrorCode);

        //        if (await _personRepository.DeletePerson(PersonID))
        //        {
        //            ImageHandler.DeleteImage(PersonResult.Data.ImagePath);
        //            return Result<bool>.Success(true);
        //        }

        //        return Result<bool>.Failure("Database delete operation failed", ErrorCode.DatabaseFailure);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogError(ex);
        //        return Result<bool>.Failure("An internal error occurred while deleting the person.", ErrorCode.InternalError);
        //    }
        //}

        //public async Task<Result<List<Person>>> GetAllPeople(PersonFillterDTO PersonDTO)
        //{
        //    try
        //    {
        //        List<Person> persons = await _personRepository.GetAllPeople(PersonDTO);
        //        return Result<List<Person>>.Success(persons);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogError(ex);
        //        return Result<List<Person>>.Failure("An internal error occurred while retrieving persons.", ErrorCode.InternalError);
        //    }
        //}

        //public Task<bool> IsPersonExist(int PersonID)
        //{
        //    return _personRepository.IsPersonExists(PersonID);
        //}


        //internal Task<bool> HasPhoneNumber(int PersonID)
        //{
        //    return _personData.HasPhoneNumber(PersonID);
        //}
    }
}

