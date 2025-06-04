using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
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
        public static ExtentTest Test;
        private static string ScreenShotDefaultPath = "C:\\Automation\\Reports\\Images\\";

        public static void InitReport()
        {
            var htmlReporter = new ExtentSparkReporter("C:\\Automation\\Reports\\report.html");
            Extent = new ExtentReports();
            Extent.AttachReporter(htmlReporter);
        }

        public static void CreateTest(string testName)
        {
            Test = Extent.CreateTest(testName);
        }

        public static void LogStep(string message, bool takeScreenshot = false, IWebDriver driver = null)
        {
            if (takeScreenshot)
            {
                //Test.Log(Status.Info, message).AddScreenCaptureFromPath(CaptureScreenshot());
                Test.Log(Status.Info, message, MediaEntityBuilder.CreateScreenCaptureFromPath(CaptureScreenshot2(driver)).Build());

            }
            else
            {
                Test.Log(Status.Info, message);
            }
        }

        public static void LogError(string message, bool takeScreenshot = false, IWebDriver driver = null)
        {
            if (takeScreenshot)
            {
                //Test.Fail(message).AddScreenCaptureFromPath(screenshotPath);
                Test.Fail(MediaEntityBuilder.CreateScreenCaptureFromPath(CaptureScreenshot2(driver)).Build());
            }
            else
            {
                Test.Fail(message);
            }
        }

        private static string CaptureScreenshot(IWebDriver driver)
        {
            var fileName = $"{ScreenShotDefaultPath}screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            

            // Guardar el archivo
            string screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "screenshot.png");
            screenshot.SaveAsFile(screenshotPath);
            return fileName;
        }

        private static string CaptureScreenshot2(IWebDriver driver)
        {
            // Tomar el screenshot
            var screenshotPath = $"{ScreenShotDefaultPath}screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(screenshotPath);
            return screenshotPath;
        }

        public static void FlushReport()
        {
            Extent.Flush();
        }




    }
}
