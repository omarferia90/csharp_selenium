using AutomationFramework.Base;
using AutomationFramework.Pages;
using AutomationFramework.Reports;
using AutomationFramework.Utilities;
using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests
{
    [TestFixture]
    public class LoginTests2 : BaseTest
    {

        [Test]
        public void ValidLoginTest()
        {
            Driver.Navigate().GoToUrl(EnvironmentConfig.BaseUrl);
            var loginPage = new LoginPage(Driver);



            ReportManager.LogStep(Status.Info, "test message", Driver, true);
            ReportManager.LogStep(Status.Pass, "test message", Driver, true);
            ReportManager.LogStep(Status.Warning, "test message", Driver, true);
            ReportManager.LogStep(Status.Error, "test message", Driver, true);
            ReportManager.LogStep(Status.Fail, "test message", Driver, true);
            ReportManager.LogStep(Status.Skip, "test message", Driver, true);



            loginPage.Login("admin", "password");
            Assert.IsTrue(Driver.Url.Contains("dashboard"));
        }

        [Test]
        public void ValidLoginTest1()
        {
            Driver.Navigate().GoToUrl("https://www.google.com/");
            var loginPage = new LoginPage(Driver);



            ReportManager.LogStep(Status.Info, "test message", Driver, true);
            ReportManager.LogStep(Status.Pass, "test message", Driver, true);
            ReportManager.LogStep(Status.Warning, "test message", Driver, true);
            ReportManager.LogStep(Status.Error, "test message", Driver, true);
            ReportManager.LogStep(Status.Fail, "test message", Driver, true);
            ReportManager.LogStep(Status.Skip, "test message", Driver, true);



            loginPage.Login("admin", "password");
            Assert.IsTrue(Driver.Url.Contains("dashboard"));
        }

        [Test]
        public void ValidLoginTest2()
        {
            Driver.Navigate().GoToUrl("https://www.mercadolibre.com.mx/");
            var loginPage = new LoginPage(Driver);



            ReportManager.LogStep(Status.Info, "test message", Driver, true);
            ReportManager.LogStep(Status.Pass, "test message", Driver, true);
            ReportManager.LogStep(Status.Warning, "test message", Driver, true);
            ReportManager.LogStep(Status.Error, "test message", Driver, true);
            ReportManager.LogStep(Status.Fail, "test message", Driver, true);
            ReportManager.LogStep(Status.Skip, "test message", Driver, true);



            loginPage.Login("admin", "password");
            Assert.IsTrue(Driver.Url.Contains("dashboard"));
        }



    }

}
