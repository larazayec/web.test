using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

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

        public void Alert()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
             .Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }

        public void Registration(string login, string email, string password, string confirmpassword)
        {
            LoginField.SendKeys(login);
            EmailField.SendKeys(email);
            PasswordField.SendKeys(password);
            ConfirmField.SendKeys(confirmpassword);
        }

        public bool IsOpened
        {
            get
            {
                WaitForReady();
                return driver.Url == "https://localhost:5001/";
            }
        }

        private void WaitForReady()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                wait.Until(ExpectedConditions.ElementToBeClickable(RegisterButton));
            }
            catch (Exception ex)
            {

            }
        }

        public void Open()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.Open();
            loginPage.RegisterButton.Click();
            WaitForReady();
        }

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
    }
}
