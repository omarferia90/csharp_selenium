using AutomationFramework.Reports;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;
using AventStack.ExtentReports;

namespace AutomationFramework.Utilities
{
    public class WebElementHelper
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public WebElementHelper(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public IWebElement FindElement(By locator)
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public void Click(By locator)
        {
            try
            {
                var element = FindElement(locator);
                element.Click();
                ReportManager.LogStep(Status.Info, $"Clicked on element: {locator}", _driver);
            }
            catch (Exception e)
            {
                ReportManager.LogStep(Status.Fail, $"Click failed: {e.Message}", _driver, true);
                throw;
            }
        }

        public void SelectDropdown(By locator, string value)
        {
            var dropdown = new SelectElement(FindElement(locator));
            dropdown.SelectByText(value);
            ReportManager.LogStep(Status.Info, $"Selected dropdown value: {value}", _driver);
        }

        public void DoubleClick(By locator)
        {
            var actions = new Actions(_driver);
            var element = FindElement(locator);
            actions.DoubleClick(element).Perform();
            //ReportManager.LogStep($"Double clicked element: {locator}");
            ReportManager.LogStep(Status.Info, $"Double clicked element: {locator}", _driver);
        }
    }
}
