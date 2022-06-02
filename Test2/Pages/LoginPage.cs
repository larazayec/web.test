using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2.Pages
{
    internal class LoginPage
    {
        private IWebDriver driver;
        public LoginPage(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        public IWebElement LoginField
        {
            get
            {
                return driver.FindElement(By.XPath("//th[text()='Login:']/..//input"));
            }
        }

        public IWebElement PasswordField => driver.FindElement(By.XPath("//th[text()='Password:']/..//input"));
        public IWebElement LoginButton => driver.FindElement(By.Id("loginBtn"));
        public string Error
        {
            get
            {
                By locator = By.Id("errorMessage");
                new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(ExpectedConditions.ElementIsVisible(locator));
                return driver.FindElement(locator).Text;
            }
        }

        public void Login(string login, string password)
        {
            LoginField.SendKeys(login);
            PasswordField.SendKeys(password);
            LoginButton.Click();
        }
    }
}
