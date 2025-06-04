using AutomationFramework.Utilities;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Reports
{
    public static class ReportManager
    {
        public static ExtentReports Extent;
        [ThreadStatic] public static ExtentTest Test;
        public static string ReportFolder = "";
        public static string ReportFolderPath = "";

        /// <summary>
        /// Inits ExtentReports with prefilled configuration
        /// </summary>
        public static void InitReport()
        {
            ReportFolder = $"Execution_{DateTime.Now.ToString("MMddyyyy_hhmmss")}";
            ReportFolderPath = $"{EnvironmentConfig.ReportPath}{ReportFolder}";
            ReportFolderSetup();

            var htmlReporter = new ExtentSparkReporter($"{ReportFolderPath}\\report.html");
            //var htmlReporter = new ExtentSparkReporter($"C:\\Automation\\Reports\\report.html");
        
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark;
            Extent = new ExtentReports();
            Extent.AddSystemInfo("Project", "Automation Framework");
            Extent.AddSystemInfo("Browser", EnvironmentConfig.Browser);
            Extent.AddSystemInfo("Env", EnvironmentConfig.TestEnvironment);
            Extent.AddSystemInfo("Executed By", Environment.UserName.ToString());
            Extent.AddSystemInfo("Execution Time", DateTime.Now.ToString("MM/ddy/yyy hh:mm:ss"));
            Extent.AttachReporter(htmlReporter);
        }

        /// <summary>
        /// Creates a test case for ExtentReports
        /// </summary>
        /// <param name="testName"></param>
        public static void CreateTest(string testName)
        {
            Test = Extent.CreateTest(testName);
        }

        
        /// <summary>
        /// Logs a step during execution time
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="driver"></param>
        /// <param name="takeScreenshot"></param>
        public static void LogStep(Status status, string message, IWebDriver driver = null, bool takeScreenshot = false)
        {
            if (takeScreenshot)
            {
                if (status == Status.Fail)
                {
                    Test.Log(status, new Exception(message), MediaEntityBuilder.CreateScreenCaptureFromPath(CaptureScreenshot2(driver)).Build());
                }
                else
                {
                    Test.Log(status, message, MediaEntityBuilder.CreateScreenCaptureFromPath(CaptureScreenshot2(driver)).Build());
                }
            }
            else
            {
                if (status == Status.Fail){
                    Test.Log(status, new Exception(message));
                }
                else {
                    Test.Log(status, message);
                }
                
            }
        }

        /// <summary>
        /// Take a screenshot of current step
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        private static string CaptureScreenshot2(IWebDriver driver)
        {
            // Tomar el screenshot
            var screenshotPath = $"{ReportFolderPath}\\ScreenShots\\screenshot_{DateTime.Now:MMddyyyy_hhmmss}.png";
            //var screenshotPath = $"C:\\Automation\\Reports\\ScreenShots\\screenshot_{DateTime.Now:MMddyyyy_hhmmss}.png";
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(screenshotPath);
            return screenshotPath;
        }

        /// <summary>
        /// Generates the report with all results
        /// </summary>
        public static void FlushReport()
        {
            Extent.Flush();
        }

        /// <summary>
        /// Generates Base Folder for Report
        /// </summary>
        private static void ReportFolderSetup()
        {
            //Creates Folder for current execution
            bool blFolderExist = System.IO.Directory.Exists(ReportFolderPath);
            if (!blFolderExist)
            {
                System.IO.Directory.CreateDirectory(ReportFolderPath);
            }
            else
                blFolderExist = false;
            
            //Creates Sub Folders that are necesary
            string[] arrSubFolders = new string[2] { "ScreenShots", "Attachments" };
            foreach (string strFolder in arrSubFolders)
            {
                blFolderExist = System.IO.Directory.Exists($"{ReportFolderPath}//{strFolder}");

                if (!blFolderExist)
                {
                    System.IO.Directory.CreateDirectory($"{ReportFolderPath}//{strFolder}");
                }
            }
        }



    }
}
