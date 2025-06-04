using AutomationFramework.Base;
using AutomationFramework.Pages;
using AutomationFramework.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests
{
    [TestFixture]
    public class LoginTests : BaseDriver
    {
        [SetUp]
        public void Setup()
        {
            InitDriver(BrowserType.Chrome);
            ReportManager.InitReport();
            ReportManager.CreateTest("Login Test");
        }

        [Test]
        public void ValidLoginTest()
        {
            Driver.Navigate().GoToUrl("https://www.amazon.com.mx/");
            var loginPage = new LoginPage(Driver);
            ReportManager.LogStep("test message", true, Driver);
            loginPage.Login("admin", "password");
            Assert.IsTrue(Driver.Url.Contains("dashboard"));
        }

        [TearDown]
        public void Teardown()
        {
            QuitDriver();
            ReportManager.FlushReport();
        }
    }

}
