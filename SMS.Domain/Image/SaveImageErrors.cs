using SMS.Domain.Enums;
using SMS.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Domain.Image;

public static class SaveImageErrors
{
    public static readonly ServiceError Empty = new("Please select an image.");
    public static readonly ServiceError TooLarge = new("Image size exceeded the maximum limit.");
    public static readonly ServiceError InvalidType = new("Unsupported image format");
}

public static class GetImageErrors
{
    public static readonly ServiceError NotFound = new("The requested image was not found.");
}
 