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
    internal class SettingPage
    {
        private IWebDriver driver;
        public SettingPage(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        public IWebElement DateFormat => driver.FindElement(By.XPath("//th[text()='Date format:']/..//select[@id='dateFormat']"));
        public IWebElement DefaultCurrency => driver.FindElement(By.XPath("//th[text()='Default currency:']/..//select[@id='currency']"));
        public IWebElement NumberFormat => driver.FindElement(By.XPath("//th[text()='Number format:']/..//select[@id='numberFormat']"));
        public IWebElement ButtonSave => driver.FindElement(By.Id("save"));
        public IWebElement LogutButton => driver.FindElement(By.XPath("//div[contains(@class, 'login')]"));

        public string EndDay => driver.FindElement(By.XPath("//th[text()='End Date: *']/..//input")).GetAttribute("value");

        private void WaitForReady()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                wait.Until(ExpectedConditions.ElementToBeClickable(ButtonSave));
            }
            catch (Exception ex)
            {

            }
        }
        public void Сonfigure(string format)
        {
            SelectElement dateFormSeletct = new SelectElement(DateFormat);
            dateFormSeletct.SelectByText(format);
            WaitForReady();
            ButtonSave.Click();
            WaitForReady();

        }
        public List<string> Defaultcurrenc
        {
            get
            {
                List<string> actuale = new List<string>();
                SelectElement currencySeletct = new SelectElement(DefaultCurrency);
                foreach (IWebElement element in currencySeletct.Options)
                {
                    actuale.Add(element.Text);
                }
                return actuale;
            }
        }

    }

}
