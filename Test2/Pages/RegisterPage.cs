using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2.Pages
{
    internal class RegisterPage
    {
        private IWebDriver driver;
        public RegisterPage(IWebDriver webDriver)
        {
            driver = webDriver;
        }
        public IWebElement LoginField => driver.FindElement(By.Id("login"));
        public IWebElement EmailField => driver.FindElement(By.Id("email"));
        public IWebElement PasswordField => driver.FindElement(By.Id("password1"));
        public IWebElement ConfirmField => driver.FindElement(By.Id("password2"));
        public IWebElement RegisterButton => driver.FindElement(By.Id("register"));
        public IWebElement ReturnLoginButton => driver.FindElement(By.XPath("//div[contains(@class, 'login link btn btn-link')]"));

        public void Registration(string login, string email, string password, string confirmpassword)
        {
            LoginField.SendKeys(login);
            EmailField.SendKeys(email);
            PasswordField.SendKeys(password);
            ConfirmField.SendKeys(confirmpassword);
            RegisterButton.Click();
        }

    }
}
