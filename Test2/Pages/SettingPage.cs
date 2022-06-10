using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
        
        public List<string> CurrencyName
        { 
            get
            {
                List<string> actuale = new List<string>();
                SelectElement DefaultCurrencySeletct = new SelectElement(DefaultCurrency);
                foreach (IWebElement element in DefaultCurrencySeletct.Options)
                {
                    actuale.Add(element.Text);
                }
                return actuale;
            }
        }
            
    }

}
