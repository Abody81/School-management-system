using System.Runtime.CompilerServices;

namespace SMS.Business.Util
{
    internal class Logger
    {
        private static readonly string _logFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "Services logs");

        public static void LogError(Exception ex, [CallerMemberName] string methodName = "", [CallerFilePath] string filePath = "")
        {
            if (!Directory.Exists(_logFolderPath))
            {
                Directory.CreateDirectory(_logFolderPath);
            }

            string className = Path.GetFileNameWithoutExtension(filePath);

            string ErrorMessage = $"{{{DateTime.Now}}} (Class: {className}) In ({methodName}):{Environment.NewLine}{ex.Message}.\n";

            string TextPath = Path.Combine(_logFolderPath, "Services.txt");
            File.AppendAllText(TextPath, ErrorMessage);
        }
    }
}
