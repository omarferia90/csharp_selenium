using AutomationFramework.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages
{
    public abstract class BasePage
    {
        protected IWebDriver Driver;
        protected WebElementHelper WEHelper;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            WEHelper = new WebElementHelper(driver);
        }
    }
}
