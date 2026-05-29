using SMS.Application.UseCases.People.Commands.CreatePerson;
using SMS.Application.UseCases.People.Queries.GetAllPeople;
using SMS.Domain.Entities;
using SMS.Domain.Enums;
using SMS.Domain.Image;
using SMS.Domain.Util;

namespace SMS.Application.UseCases.People;

public  static  class PersonErrors
{
    public static readonly Error EmptyImage = new Error(ErrorType.Validation, "EMPTY_IMAGE",
        "Please select an image.", 
        nameof(CreatePersonCommand.Image));

    public static readonly Error EmptyImagePath = new Error(ErrorType.Validation, "EMPTY_IMAGE_PATH",
        "Image path is empty",
        nameof(CreatePersonCommand.Image));

    public static readonly Error ImageExceedsSizeLimit = new Error(ErrorType.SizeExceedsLimit, "IMAGE_EXCEEDS_SIZE_LIMIT",
        "Image size exceeds the maximum allowed limit", 
        nameof(CreatePersonCommand.Image),
        new() { { "MaxImageSize", Rules.MaxImageSizeBytes } });

    public static readonly Error InvalidImageType = new Error(ErrorType.UnsupportedMediaType, "INVALID_IMAGE_TYPE",
        "Please select a valid image type.", 
        nameof(CreatePersonCommand.Image));

    public static readonly Error ImageNotFound = new Error(ErrorType.NotFound, "IMAGE_NOT_FOUND",
        "Image not found in system.",
        nameof(CreatePersonCommand.Image));

    public static readonly Error PhoneNumberAlreadyExists = new Error(ErrorType.AlreadyExists, "PHONE_NUMBER_ALREADY_EXIST",
        "Phone number already exists", 
        nameof(Person.PhoneNumber));

    public static readonly Error NationalNumberAlreadyExists = new Error(ErrorType.AlreadyExists, "NATIONAL_NUMBER_ALREADY_EXIST",
        "National number already exists",
        nameof(Person.NationalNumber));

    public static readonly Error PersonNotFound = new Error(ErrorType.NotFound, "PERSON_NOT_FOUND",
        "Person not found in system!",
        "PersonId");

    public static readonly Error InvalidId = new Error(ErrorType.Validation, "INVALID_PERSON_ID",
     "Invalid person id!",
     "PersonId");

    public static readonly Error PhoneNumberReserved = new Error(ErrorType.Reserved, "PHONE_NUMBER_RESERVED",
        "Phone number is taken from another person",
        nameof(Person.PhoneNumber));

    public static readonly Error NationalNumberReserved = new Error(ErrorType.Reserved, "NATIONAL_NUMBER_RESERVED",
      "National number is taken from another person",
      nameof(Person.NationalNumber));

    public static readonly Error InternalError = new Error(ErrorType.InternalError, "INTERNAL_ERROR",
       "An internal error occurred.");

    public static readonly Error DatabaseFailure = new Error(ErrorType.InternalError, "DATABASE_FAILURE",
      "An internal error occurred in database.");

    public static readonly Error InvalidPageSize = new Error(ErrorType.Validation, "INVALID_PAGE_SIZE", 
        "Page size must be greater than zero.", field: nameof(GetAllPeopleQuery.pageSize));
}
