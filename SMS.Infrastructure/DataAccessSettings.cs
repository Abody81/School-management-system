using System.Configuration;
using System.Runtime.CompilerServices;

namespace SMS.Infrastructure
{
    public static class DataAccessSettings
    {
        //public static async void InitialzConnectionAsync()
        //{
        //    HelperSetting.ConnectionString = ConfigurationManager.ConnectionStrings["SchoolDbConn"].ConnectionString;

        //    object obj = await ADO_Helper.ExecuteScalarAsync("select 1");
        //}

        private static readonly string _logFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "Data Access logs");

        public static void LogError(Exception ex, [CallerMemberName] string methodName = "", [CallerFilePath] string filePath = "")
        {
            if (!Directory.Exists(_logFolderPath))
            {
                Directory.CreateDirectory(_logFolderPath);
            }

            string className = Path.GetFileNameWithoutExtension(filePath);
            
            string ErrorMessage = $"{{{DateTime.Now}}} (Class: {className}) In ({methodName}):{Environment.NewLine}{ex.Message}.\n";

            string TextPath = Path.Combine(_logFolderPath, "DataAccess.txt");
            File.AppendAllText(TextPath, ErrorMessage);
        }
    }
}
