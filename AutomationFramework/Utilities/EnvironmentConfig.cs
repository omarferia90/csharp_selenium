using DotNetEnv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Utilities
{
    public static class EnvironmentConfig
    {
        static EnvironmentConfig()
        {
            Env.Load(); // Carga el archivo .env automáticamente
        }

        public static string TestEnvironment =>
            Environment.GetEnvironmentVariable("TEST_ENVIRONMENT") ?? "QA";
        public static string Browser =>
            Environment.GetEnvironmentVariable("BROWSER") ?? "Chrome";

        public static string BaseUrl =>
            Environment.GetEnvironmentVariable("BASE_URL") ?? "https://www.amazon.com.mx/";

        public static string ExcelPath =>
            Environment.GetEnvironmentVariable("EXCEL_PATH") ?? "TestData.xlsx";

        public static string ReportPath =>
            Environment.GetEnvironmentVariable("REPORT_PATH") ?? "C:\\Automation\\Reports\\";
    }
}
