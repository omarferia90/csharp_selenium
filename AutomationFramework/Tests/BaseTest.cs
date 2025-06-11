using AutomationFramework.Base;
using AutomationFramework.Reports;
using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests
{
    [TestFixture]
    public abstract class BaseTest : BaseDriver
    {
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            //Verify .runsettings is loaded
            string currentDirectory = TestContext.CurrentContext.WorkDirectory;
            string projectRoot = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            string runSettingsFile = Path.Combine(projectRoot, "Env.runsettings");
            if (!File.Exists(runSettingsFile))
            {
                throw new FileNotFoundException($"El archivo '{runSettingsFile}' no fue encontrado en el directorio raíz del proyecto.");
            }

            ReportManager.InitReport();
        }

        [SetUp]
        public void TestSetup()
        {
            InitDriver(GetBrowserType());
            ReportManager.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void TestTearDown()
        {
            var outcome = TestContext.CurrentContext.Result.Outcome.Status;
            if (outcome == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                ReportManager.LogStep(Status.Fail, $"Test failed: {TestContext.CurrentContext.Result.Message}", Driver);
            }

            QuitDriver();
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            ReportManager.FlushReport();
        }
    }
}
