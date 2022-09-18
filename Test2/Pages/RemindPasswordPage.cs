using OpenQA.Selenium;
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

        public IWebElement ErrorMassage => (IWebElement)driver.SwitchTo().Frame(driver.FindElement(By.Id("message")));

    }
}
