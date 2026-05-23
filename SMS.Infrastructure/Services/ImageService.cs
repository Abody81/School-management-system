using FileSignatures;
using FileSignatures.Formats;
using SMS.Application.Interfaces;
using SMS.Application.Interfaces.Repositories;
using SMS.Domain.Image;
using SMS.Domain.Util;

namespace SMS.Infrastructure.Services;

public class ImageService : IImageService
{
    private readonly string _folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictures");

    private const long MaxFileSizeBytes = Rules.MaxImageSizeBytes;

    public async Task<ServiceResult<string>> SaveImageAsync(Stream imageStream)
    {
        if (imageStream == null || imageStream.Length == 0)
            return SaveImageErrors.Empty;

        if (imageStream.CanSeek) imageStream.Position = 0;

        if (imageStream.Length > MaxFileSizeBytes)
            return SaveImageErrors.TooLarge;

        FileFormatInspector inspector = new FileFormatInspector();
        FileFormat? format = inspector.DetermineFileFormat(imageStream);

        if (format is null || format is not Image)
            return SaveImageErrors.InvalidType;

        if (imageStream.CanSeek) imageStream.Position = 0;

        Directory.CreateDirectory(_folderPath);

        string Extension = format!.Extension;
        string fileName = $"{Guid.NewGuid()}.{Extension}";
        string destinationPath = Path.Combine(_folderPath, fileName);

        await using var fileStream = new FileStream(destinationPath, FileMode.Create);

        await imageStream.CopyToAsync(fileStream);

        return fileName;
    }

    public void DeleteImage(string fileName)
    {
        string fullPath = Path.Combine(_folderPath, fileName);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);    
        }
    }

    public async Task<ServiceResult<Stream>> GetImage(string imagePath)
    {
        string safeFileName = Path.GetFileName(imagePath); // for security very important  

        string fullPath = Path.Combine(_folderPath, imagePath);

        if (!File.Exists(fullPath))
            return GetImageErrors.NotFound;

        var fileStream = new FileStream(
        fullPath,
        FileMode.Open,
        FileAccess.Read,
        FileShare.Read, // يسمح لمستخدمين آخرين بقراءة نفس الصورة بنفس الوقت دون قفلها
        bufferSize: 4096,
        useAsync: true);

        return fileStream;
    }

}
