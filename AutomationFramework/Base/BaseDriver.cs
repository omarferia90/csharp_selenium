using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Base
{
    public enum BrowserType { Chrome, Firefox, Edge }

    public abstract class BaseDriver
    {
        protected IWebDriver Driver;

        public BrowserType GetBrowserType()
        {
            string browserFromSettings = TestContext.Parameters["BrowserName"];

            if (Enum.TryParse<BrowserType>(browserFromSettings, true, out var browserEnum))
            {
                return browserEnum;
            }
            else
            {
                throw new ArgumentException($"El valor '{browserFromSettings}' no corresponde a un BrowserType válido.");
            }
        }

        /// <summary>
        /// Inits Driver Instance
        /// </summary>
        public virtual void InitDriver(BrowserType browser)
        {
            switch (browser)
            {
                case BrowserType.Chrome:
                    Driver = new ChromeDriver();
                    break;
                case BrowserType.Firefox:
                    Driver = new FirefoxDriver();
                    break;
                case BrowserType.Edge:
                    Driver = new EdgeDriver();
                    break;
                default:
                    throw new ArgumentException("Unsupported browser type");
            }

            Driver.Manage().Window.Maximize();
            //Time to wait to load fully page
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            //time to wait to locate elements
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        /// <summary>
        /// Quit Driver Instance
        /// </summary>
        public virtual void QuitDriver() {
            Driver.Quit();
        }
    }






}
