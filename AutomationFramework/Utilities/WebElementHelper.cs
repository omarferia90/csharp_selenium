using AutomationFramework.Reports;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace AutomationFramework.Utilities
{
    public class WebElementHelper
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly TimeSpan _timeout;

        public WebElementHelper(IWebDriver driver)
        {
            _driver = driver;
            _timeout = TimeSpan.FromSeconds(15);
            _wait = new WebDriverWait(_driver, _timeout);
        }

        // ********************************************************************************************
        // **          Native Functions for Selenium
        // ********************************************************************************************

        public IWebElement GetElement(By locator, string elementDescription)
        {
            try 
            {
                return _wait.Until(ExpectedConditions.ElementIsVisible(locator));
            }
            catch (Exception ex) 
            {
                ReportManager.LogStep(Status.Fail, $"Find WebElement \"{elementDescription}\" failed: {ex.Message}", _driver, true);
                return null; 
            }
        }

        public IList<IWebElement> GetElements(By locator, string elementDescription)
        {
            try
            {
                // Wait until at least one element is present (but not necessarily visible)
                return _wait.Until(driver =>
                {
                    var elements = driver.FindElements(locator);
                    return (elements != null && elements.Count > 0) ? elements : null;
                });
            }
            catch (Exception ex)
            {
                ReportManager.LogStep(Status.Fail, $"Find WebElements \"{elementDescription}\" failed: {ex.Message}", _driver, true);
                return new List<IWebElement>(); // return empty list on failure instead of null
            }
        }

        public void Clear(IWebElement element, string elementDescription, bool screenshot = false)
        {
            ReportManager.LogStep(Status.Info, $"Step: Clear Element \"{elementDescription}\"");
            if (element == null)
            {
                ReportManager.LogStep(Status.Fail, "Clear failed: Element is null", _driver, true);
                throw new ArgumentNullException(nameof(element), "Element passed to Clear is null");
            }

            try
            {
                element.Clear();
                Thread.Sleep(TimeSpan.FromSeconds(2));
                ReportManager.LogStep(Status.Pass, $"Clear on element was done successfully", _driver, screenshot);
            }
            catch (Exception ex)
            {
                ReportManager.LogStep(Status.Fail, $"Clear failed: {ex.Message}.", _driver, true);
                throw;
            }
        }

        public void Click(IWebElement element, string elementDescription, bool screenshot = false)
        {
            ReportManager.LogStep(Status.Info, $"Step: Click Element \"{elementDescription}\"");
            if (element == null)
            {
                ReportManager.LogStep(Status.Fail, "Click failed: Element is null", _driver, true);
                throw new ArgumentNullException(nameof(element), "Element passed to Click is null");
            }

            try
            {
                element.Click();
                ReportManager.LogStep(Status.Pass, $"Clicked on element was done successfully", _driver, screenshot);
            }
            catch (Exception ex)
            {
                ReportManager.LogStep(Status.Fail, $"Click failed: {ex.Message}.", _driver, true);
                throw;
            }
        }

        public void DoubleClick(IWebElement element, string elementDescription, bool screenshot = false)
        {
            ReportManager.LogStep(Status.Info, $"Step: Double Click Element \"{elementDescription}\"");
            if (element == null)
            {
                ReportManager.LogStep(Status.Fail, "Double Click failed: Element is null", _driver, true);
                throw new ArgumentNullException(nameof(element), "Element passed to Double Click is null");
            }

            try
            {
                var actions = new Actions(_driver);
                actions.DoubleClick(element).Perform();
                ReportManager.LogStep(Status.Pass, $"Double Clicked  on element was done successfully", _driver, screenshot);

            }
            catch (Exception ex)
            {
                ReportManager.LogStep(Status.Fail, $"Double Click failed: {ex.Message}.", _driver, true);
                throw;
            }
        }

        public void SendKeys(IWebElement element, string elementDescription, string stringValue, bool screenshot = false)
        {
            ReportManager.LogStep(Status.Info, $"Step: Sendkeys Element \"{elementDescription}\".");
            if (element == null)
            {
                ReportManager.LogStep(Status.Fail, $"Sendkey Element is null", _driver, true);
                throw new ArgumentNullException(nameof(element), "Element passed to Sendkeys is null");
            }

            try
            {
                element.Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                element.SendKeys(stringValue);
                ReportManager.LogStep(Status.Pass, $"Sendkeys on element was done successfully", _driver, screenshot);
            }
            catch (Exception ex)
            {
                ReportManager.LogStep(Status.Fail, $"Sendkeys failed: {ex.Message}.", _driver, true);
                throw;
            }
        }

        public void CustomSendKeys(IWebElement element, string elementDescription, string stringValue, bool screenshot = false)
        {
            ReportManager.LogStep(Status.Info, $"Step: Sendkeys Element \"{elementDescription}\"");
            if (element == null)
            {
                ReportManager.LogStep(Status.Fail, $"Sendkeys Element is null", _driver, true);
                throw new ArgumentNullException(nameof(element), "Element passed to Sendkeys is null");
            }

            try
            {
                var actions = new Actions(_driver);
                element.Click();
                actions.KeyDown(Keys.Control).SendKeys(Keys.Home).Perform();
                element.Clear();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                element.SendKeys(Keys.Delete);
                element.SendKeys(stringValue);
                ReportManager.LogStep(Status.Pass, $"Sendkeys on element was done successfully", _driver, screenshot);
            }
            catch (Exception ex)
            {
                ReportManager.LogStep(Status.Fail, $"Sendkeys failed: {ex.Message}.", _driver, true);
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
                ReportManager.LogStep(Status.Pass, $"Get Attribute Element: {elementDescription}");
                return element.GetAttribute(attribute);
            }
            catch (Exception ex)
            {
                ReportManager.LogStep(Status.Fail, $"Get Attribute failed: {ex.Message}.", _driver, true);
                return null;
            }
        }

        public void SelectDropdown(IWebElement element, string elementDescription, string selectMethod, string stringValue, bool screenshot = false)
        {
            ReportManager.LogStep(Status.Info, $"Step: Select Dropdown Element \"{elementDescription}\".");
            if (element == null)
            {
                ReportManager.LogStep(Status.Fail, $"Dropdown Element is null", _driver, true);
                throw new ArgumentNullException(nameof(element), "Element passed to Select Dropdown is null");
            }

            try
            {
                var selectElement = new SelectElement(element);
                switch (selectMethod.ToUpper())
                {
                    case "VALUE":
                        selectElement.SelectByValue(stringValue);
                        break;
                    case "TEXT":
                        selectElement.SelectByText(stringValue);
                        break;
                    case "INDEX":
                        selectElement.SelectByIndex(Int32.Parse(stringValue));
                        break;
                }
                ReportManager.LogStep(Status.Pass, $"Select Dropdown on element was done successfully", _driver, screenshot);
            }
            catch (Exception ex)
            {
                ReportManager.LogStep(Status.Fail, $"Select Dropdown failed: {ex.Message}.", _driver, true);
                throw;
            }
        }



        // ********************************************************************************************
        // **          Functions for Expexted Conditions
        // ********************************************************************************************

        public void WaitPageToLoad(By locator, TimeSpan? timeout = null)
        {
            try
            {
                ReportManager.LogStep(Status.Info, $"Waiting to load the page");
                IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                IWait<IWebDriver> wait = new WebDriverWait(_driver, timeout ?? _timeout);
                wait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
            }
            catch (Exception ex)
            {
                ReportManager.LogStep(Status.Fail, $"The Page was not loaded and some issues were found. {ex.Message}");
            }
        }

        public bool WaitUntilElementPresent(By locator, TimeSpan? timeout = null) 
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, timeout ?? _timeout);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
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
                WebDriverWait wait = new WebDriverWait(_driver, timeout ?? _timeout);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
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
                WebDriverWait wait = new WebDriverWait(_driver, timeout ?? _timeout);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
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
                WebDriverWait wait = new WebDriverWait(_driver, timeout ?? _timeout);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
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
                WebDriverWait wait = new WebDriverWait(_driver, timeout ?? _timeout);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeSelected(locator));
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
                WebDriverWait wait = new WebDriverWait(_driver, timeout ?? _timeout);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
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
                WebDriverWait wait = new WebDriverWait(_driver, timeout ?? _timeout);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementWithText(locator, partialText));
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
                WebDriverWait wait = new WebDriverWait(_driver, timeout ?? _timeout);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains(partialText));
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
                WebDriverWait wait = new WebDriverWait(_driver, timeout ?? _timeout);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains(partialText));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }





    }
}
