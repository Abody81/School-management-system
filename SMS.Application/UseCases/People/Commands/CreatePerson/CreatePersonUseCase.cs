using Microsoft.Extensions.Logging;
using SMS.Application.Interfaces;
using SMS.Application.Interfaces.Repositories;
using SMS.Domain.Entities;
using SMS.Domain.Enums;
using SMS.Domain.Image;
using SMS.Domain.Util;

namespace SMS.Application.UseCases.People.Commands.CreatePerson;

public class CreatePersonUseCase(
    IPersonRepository _repo,
    IImageService _imageService,
    ILogger<CreatePersonUseCase> _logger,
    IUnitOfWork _uow
    )
{

    public async Task<Result<int>> ExecuteAsync(CreatePersonCommand personCommand)
    {
        string? savedImagePath = null;

        try
        {
            var ValidateResult = await UniqueValidationAsync(personCommand);
            if (!ValidateResult.IsSuccess) return Result<int>.Failure(ValidateResult);

            var ImageResult = await HandleImage(personCommand.Image);
            if (!ImageResult.IsSuccess) return Result<int>.Failure(ImageResult);

            savedImagePath = ImageResult.Data!; 

            Person person = Person.Create(personCommand.FirstName,personCommand.SecondName,personCommand.ThirdName,
                personCommand.LastName,personCommand.Gender,personCommand.DateOfBirth,
                personCommand.NationalNumber,personCommand.PhoneNumber,personCommand.Address,
                personCommand.CountryID,savedImagePath!);

            _repo.AddPerson(person);
            await _uow.SaveChangesAsync();

            return Result<int>.Success(person.PersonID);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding person");

            _imageService.DeleteImage(savedImagePath!);

            return Result<int>.Failure(PersonErrors.InternalError);
        }
    }

    private async Task<Result> UniqueValidationAsync(CreatePersonCommand command)
    {
        var Result = new Result(ErrorType.AlreadyExists); // by default is Success but when add use AddError change to !Success

        if (!string.IsNullOrEmpty(command.PhoneNumber))
        {
            var IsPhoneNumberExist = await _repo.IsPhoneNumberExist(command.PhoneNumber);

            if (IsPhoneNumberExist)
                Result.AddError(PersonErrors.PhoneNumberAlreadyExists);
        }

        var IsNationalNumberExist = await _repo.IsNationalNumberExist(command.NationalNumber);

        if (IsNationalNumberExist)
            Result.AddError(PersonErrors.NationalNumberAlreadyExists);

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
