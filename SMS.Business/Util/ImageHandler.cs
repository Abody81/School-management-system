namespace SMS.Business.Util
{
    public static class ImageHandler
    {
        public static readonly string _folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictures");

        private static void EnsureFolderExists()
        {
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
        }

        internal static bool DeleteImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return false;

            try
            {
                string fullPath = Path.Combine(_folderPath, fileName);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
            }
            catch
            {
                throw;
            }

            return false;
        }

        internal static string GetFullPath(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return null;

            return Path.Combine(_folderPath, fileName);
        }


        private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg",    ".jpeg",   ".png",
            ".webp",   ".heic",    ".heif",
        };

        internal static bool CheckImageType(string extension)
        {
            return !string.IsNullOrEmpty(extension) && AllowedExtensions.Contains(extension);
        }

        // for internal use only.
        internal static string SaveImage(string sourceImagePath)
        {
            if (string.IsNullOrEmpty(sourceImagePath) || !File.Exists(sourceImagePath))
            {
                return null!;
            }

            try
            {
                EnsureFolderExists();

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(sourceImagePath);
                string destinationPath = Path.Combine(_folderPath, fileName);

                File.Copy(sourceImagePath, destinationPath);

                return fileName;   // نعيد الاسم فقط لنخزنه في قاعدة البيانات
            }
            catch 
            {
                throw;
            }
        }

        internal static async Task<string> SaveImageAsync(Stream imageStream, string fileExtension)
        {
            if (imageStream == null || imageStream.Length == 0 || !CheckImageType(fileExtension))
                return null;

            try
            {
                if (imageStream.CanSeek)  imageStream.Position = 0; 

                EnsureFolderExists();

                string fileName = Guid.NewGuid().ToString() + fileExtension;
                string destinationPath = Path.Combine(_folderPath, fileName);

                using (var fileStream = new FileStream(destinationPath, FileMode.Create))
                {
                    await imageStream.CopyToAsync(fileStream);
                }

                return fileName;
            }
            catch { throw; }
        }
    }
}

