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
    internal class RemindPasswordPage
    {
        private IWebDriver driver;
        public RemindPasswordPage(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        public void Frame()
        {
            driver.SwitchTo().Frame(0);
        }
        
        public IWebElement EmailField => (IWebElement)driver.SwitchTo().Frame(driver.FindElement(By.Id("email")));

        public IWebElement SendButton => (IWebElement)driver.SwitchTo().Frame(driver.FindElement(By.XPath("//div[contains('Send')]")));

        public IWebElement Message1 => (IWebElement)driver.SwitchTo().Frame(driver.FindElement(By.Id("message")));

        public void AcceptAlert()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
             .Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }

        public void RemindPassword(string email)
        {
            EmailField.SendKeys(email);
            SendButton.Click();
        }

        public string Message
        {
            get
            {
                By locator = By.Id("message");
                new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(ExpectedConditions.ElementIsVisible(locator));
                return driver.FindElement(locator).Text;
            }
        }
    }
}
