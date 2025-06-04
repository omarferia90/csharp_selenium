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
        private readonly By txtUsername = By.Id("username");
        private readonly By txtPassword = By.Id("password");
        private readonly By btnLogin = By.Id("login");

        public LoginPage(IWebDriver driver) : base(driver) { }

        public void Login(string username, string password)
        {
            Helper.Click(txtUsername);
            Driver.FindElement(txtUsername).SendKeys(username);

            Helper.Click(txtPassword);
            Driver.FindElement(txtPassword).SendKeys(password);

            Helper.Click(btnLogin);
        }
    }
}
