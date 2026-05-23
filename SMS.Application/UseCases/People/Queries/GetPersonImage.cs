using SMS.Application.Interfaces;
using SMS.Domain.Image;
using SMS.Domain.Util;

namespace SMS.Application.UseCases.People.Queries;

public class GetPersonImage(IImageService _imageService)
{
    public async Task<Result<Stream>> ExecuteAsync(string ImagePath)
    {
        if (string.IsNullOrEmpty(ImagePath))
            return PersonErrors.EmptyImagePath;

        var ImageResult = await _imageService.GetImage(ImagePath);

        if (ImageResult.IsSuccess)
            return ImageResult.Data!;

        if (ImageResult.Error == GetImageErrors.NotFound)
            return PersonErrors.ImageNotFound;

        return PersonErrors.InternalError;
    }
}