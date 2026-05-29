
using Microsoft.Extensions.Logging;
using SMS.Application.Interfaces;
using SMS.Application.Interfaces.Repositories;
using SMS.Application.UseCases.People.Commands.CreatePerson;
using SMS.Domain.Entities;
using SMS.Domain.Enums;
using SMS.Domain.Image;
using SMS.Domain.Util;

namespace SMS.Application.UseCases.People.Commands.UpdatePerson;

public class UpdatePersonUseCase(ILogger<UpdatePersonUseCase> _logger,
     IPersonRepository _repo,
     IImageService _imageService,
     IUnitOfWork _uow)
{

    public async Task<Result> ExecuteAsync(UpdatePersonCommand personCommand, CancellationToken ct)
    {
        try
        {
            Person? OldPerson = await _repo.GetPersonById(personCommand.Id, ct, true);
            if (OldPerson == null)
                return Result<bool>.Failure(PersonErrors.PersonNotFound, new() { { "PersonId", personCommand.Id } });
        
            var UniqueValidationResult = await UniqueValidationAsync(personCommand, ct);
            if (!UniqueValidationResult.IsSuccess) return Result<bool>.Failure(UniqueValidationResult);

            return await UpdatePersonAsync(personCommand ,OldPerson);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An internal error occurred while updating the person");

            return Result.Failure(PersonErrors.InternalError);
        }
    }

    private async Task<Result> UpdatePersonAsync(UpdatePersonCommand newPerson, Person oldPerson)
    {
        string? newSavedPath = null;
        string CurrentImage = string.Empty;

        bool ImageChanged = newPerson.Image != null;

        string PreviousImagePath = oldPerson.ImagePath;

        try
        {
            if (ImageChanged)
            {
                var ImageResult = await HandleImage(newPerson.Image!);
                if (!ImageResult.IsSuccess) return Result.Failure(ImageResult);

                newSavedPath = ImageResult.Data;

                CurrentImage = newSavedPath!;
            }
            else
            {
                CurrentImage = PreviousImagePath;
            }

            oldPerson.Update(newPerson.FirstName, newPerson.SecondName, newPerson.ThirdName, newPerson.LastName,
                newPerson.Gender, newPerson.DateOfBirth, newPerson.NationalNumber, newPerson.PhoneNumber,
                newPerson.Address, newPerson.CountryID, CurrentImage);

            _repo.UpdatePerson(oldPerson);

            if (await _uow.SaveChangesAsync() > 0)
            {
                if (ImageChanged)
                {
                    _imageService.DeleteImage(PreviousImagePath!);
                }
                return Result.Success();
            }

            else
            {
                if (ImageChanged && !string.IsNullOrEmpty(newSavedPath))
                {
                    _imageService.DeleteImage(newSavedPath!);
                }

                return Result.Failure(PersonErrors.InternalError);
            }

        }
        catch 
        {
            if (ImageChanged && !string.IsNullOrEmpty(newSavedPath))
                _imageService.DeleteImage(newSavedPath!);

            return Result.Failure(PersonErrors.InternalError);
        }
    }

    private async Task<Result> UniqueValidationAsync(UpdatePersonCommand person, CancellationToken ct)
    {
        var Result = new Result(ErrorType.Reserved); // by default is Success but when add use AddError change to !Success

        if (!string.IsNullOrEmpty(person.PhoneNumber))
        {
            var IsPhoneNumberExist = await _repo.IsPhoneNumberReserved(person.Id, person.PhoneNumber, ct);

            if (IsPhoneNumberExist)
                Result.AddError(PersonErrors.PhoneNumberReserved);
        }

        var IsNationalNumberExist = await _repo.IsNationalNumberReserved(person.Id, person.NationalNumber, ct);

        if (IsNationalNumberExist)
            Result.AddError(PersonErrors.NationalNumberReserved);

        return Result;
    }

    private async Task<Result<string>> HandleImage(Stream imageStream)
    {
        var ImageResult = await _imageService.SaveImageAsync(imageStream);

        return ImageResult.Match<Result<string>>(
         fileName => fileName,
         error => error switch
         {
             _ when error == SaveImageErrors.TooLarge => PersonErrors.ImageExceedsSizeLimit,
             _ when error == SaveImageErrors.InvalidType => PersonErrors.InvalidImageType,
             _ => PersonErrors.EmptyImage
         });
    }
}
