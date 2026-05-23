using SMS.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Application.Interfaces;

public interface IImageService
{
    Task<ServiceResult<string>> SaveImageAsync(Stream imageStream);

    void DeleteImage(string fileName);

    Task<ServiceResult< Stream>> GetImage(string imagePath);
}
