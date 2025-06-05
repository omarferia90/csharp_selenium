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
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;

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

        // ********************************************************************************************
        // **          Native Functions for Selenium
        // ********************************************************************************************

        public IWebElement? FindElement(By locator)
        {
            try 
            {
                return _wait.Until(ExpectedConditions.ElementIsVisible(locator));
            }
            catch (Exception ex) 
            {
                ReportManager.LogStep(Status.Fail, $"Find WebElement failed: {ex.Message}", _driver, true);
                return null; 
            }
        }

        public void Clear(IWebElement element, string elementDescription)
        {
            if (element == null)
            {
                ReportManager.LogStep(Status.Fail, "Clear failed: Element is null", _driver, true);
                throw new ArgumentNullException(nameof(element), "Element passed to Click is null");
            }

            try
            {
                element.Clear();
                Thread.Sleep(TimeSpan.FromSeconds(2));
                ReportManager.LogStep(Status.Pass, $"Clear on element: {elementDescription}");
            }
            catch (Exception ex)
            {
                ReportManager.LogStep(Status.Fail, $"Clear failed: {ex.Message}.", _driver, true);
                throw;
            }
        }

        public void Click(IWebElement element, string elementDescription)
        {
            if (element == null)
            {
                ReportManager.LogStep(Status.Fail, "Click failed: Element is null", _driver, true);
                throw new ArgumentNullException(nameof(element), "Element passed to Click is null");
            }

            try
            {
                element.Click();
                ReportManager.LogStep(Status.Pass, $"Clicked on element: {elementDescription}");
            }
            catch (Exception ex)
            {
                ReportManager.LogStep(Status.Fail, $"Click failed: {ex.Message}.", _driver, true);
                throw;
            }
        }

        public void DoubleClick(IWebElement element, string elementDescription)
        {
            if (element == null)
            {
                ReportManager.LogStep(Status.Fail, "Double Click failed: Element is null", _driver, true);
                throw new ArgumentNullException(nameof(element), "Element passed to Click is null");
            }

            try
            {
                var actions = new Actions(_driver);
                actions.DoubleClick(element).Perform();
                ReportManager.LogStep(Status.Pass, $"Double Clicked on element: {elementDescription}");
            }
            catch (Exception ex)
            {
                ReportManager.LogStep(Status.Fail, $"Double Click failed: {ex.Message}.", _driver, true);
                throw;
            }
        }

        public void ScrollMiddle(IWebElement element, string elementDescription)
        {
            if (element == null)
            {
                ReportManager.LogStep(Status.Fail, "Scroll failed: Element is null", _driver, true);
                throw new ArgumentNullException(nameof(element), "Element passed to Click is null");
            }

            try
            {
                var scrollElementIntoMiddle = "var viewPortHeight = Math.max(document.documentElement.clientHeight, window.innerHeight || 0); var elementTop = arguments[0].getBoundingClientRect().top; window.scrollBy(0, elementTop-(viewPortHeight/2));";
                ((IJavaScriptExecutor)_driver).ExecuteScript(scrollElementIntoMiddle, element);
                ReportManager.LogStep(Status.Pass, $"Scroll Middle on element: {elementDescription}");
            }
            catch (Exception ex)
            {
                ReportManager.LogStep(Status.Fail, $"Scroll Middle failed: {ex.Message}.", _driver, true);
                throw;
            }
        }

        public string? GetAttribute(IWebElement element, string elementDescription, string attribute)
        {
            if (element == null)
            {
                ReportManager.LogStep(Status.Fail, "Get Attribute failed: Element is null", _driver, true);
                throw new ArgumentNullException(nameof(element), "Element passed to Click is null");
            }

            try
            {
                ReportManager.LogStep(Status.Pass, $"Get Attribute on element: {elementDescription}");
                return element.GetAttribute(attribute);
            }
            catch (Exception ex)
            {
                ReportManager.LogStep(Status.Fail, $"Get Attribute failed: {ex.Message}.", _driver, true);
                return null;
            }
        }



        // ********************************************************************************************
        // **          Functions for Expexted Conditions
        // ********************************************************************************************

        
        public bool WaitUntilElementPresent(By locator, TimeSpan? timeout = null) 
        {
            try
            {
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool WaitUntilElementExist(By locator, TimeSpan? timeout = null)
        {
            try
            {
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool WaitUntilElementIsVisible(By locator, TimeSpan? timeout = null)
        {
            try
            {
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool WaitUntilElementToBeClickable(By locator, TimeSpan? timeout = null)
        {
            try
            {
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool WaitUntilElementToBeSelected(By locator, TimeSpan? timeout = null)
        {
            try
            {
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeSelected(locator));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool WaitUntilElementHidden(By locator, TimeSpan? timeout = null)
        {
            try
            {
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool WaitUntilElementHiddenWithText(By locator, string partialText, TimeSpan? timeout = null)
        {
            try
            {
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementWithText(locator, partialText));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool WaitUntilTitleContains(By locator, string partialText, TimeSpan? timeout = null)
        {
            try
            {
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains(partialText));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool WaitUntilUrlContains(By locator, string partialText, TimeSpan? timeout = null)
        {
            try
            {
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains(partialText));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }












    }
}
