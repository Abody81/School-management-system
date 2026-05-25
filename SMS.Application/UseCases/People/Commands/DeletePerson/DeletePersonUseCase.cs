using Microsoft.Extensions.Logging;
using SMS.Application.Interfaces;
using SMS.Application.Interfaces.Repositories;
using SMS.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Application.UseCases.People.Commands.DeletePerson;

public class DeletePersonUseCase
(
    ILogger<DeletePersonUseCase> _logger,
    IPersonRepository _repo,
    IImageService _imageService

)
{

    public async Task<Result> ExecuteAsync(DeletePersonCommand command)
    {
        if (command.PersonId < 1)
            return PersonErrors.InvalidId;

        string ?ImagePath = null;

        try
        {
            ImagePath = await _repo.GetImagePath(command.PersonId);

            if (ImagePath == null)
                return PersonErrors.PersonNotFound;

            if (await _repo.Delete(command.PersonId) > 0)
            {
                _imageService.DeleteImage(ImagePath);

                return Result.Success();
            }
            else
            {
                return PersonErrors.DatabaseFailure;
            }
        }

        catch (Exception ex) 
        {
            _logger.LogError(ex, $"An internal error occurred while deleting person with ID: {command.PersonId}");

            return PersonErrors.InternalError;
        }

    }

}
