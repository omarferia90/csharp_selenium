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
                ReportManager.LogStep($"Clicked on element: {locator}");
            }
            catch (Exception e)
            {
                ReportManager.LogError($"Click failed: {e.Message}");
                throw;
            }
        }

        public void SelectDropdown(By locator, string value)
        {
            var dropdown = new SelectElement(FindElement(locator));
            dropdown.SelectByText(value);
            ReportManager.LogStep($"Selected dropdown value: {value}");
        }

        public void DoubleClick(By locator)
        {
            var actions = new Actions(_driver);
            var element = FindElement(locator);
            actions.DoubleClick(element).Perform();
            ReportManager.LogStep($"Double clicked element: {locator}");
        }
    }
}
