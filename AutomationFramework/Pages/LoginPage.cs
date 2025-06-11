using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages
{
    public class LoginPage : BasePage
    {
        private readonly By textEmail = By.Id("ap_email_login");
        private readonly By btnContinue = By.ClassName("a-button-input");

        public LoginPage(IWebDriver driver) : base(driver) { }

        public void Login(string username, string password)
        {
            WEHelper.WaitUntilElementIsVisible(textEmail, TimeSpan.FromSeconds(15));
            WEHelper.Click(WEHelper.GetElement(textEmail, ""), "");
            WEHelper.Click(WEHelper.GetElement(btnContinue, ""), "");

        }
    }
}
