using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2.Pages
{
    internal class HistoryPage
    {
        private IWebDriver driver;
        public HistoryPage(IWebDriver webDriver)
        {
            driver = webDriver;
        }
        public IWebElement NameElement => driver.FindElement(By.XPath("//table/tr[@class='data - th']"));

        public List<string> HistoryFirst
        {
            get
            {
                List<string> actuale = new List<string>();
                SelectElement historySelect = new SelectElement(NameElement);
                foreach (IWebElement element in historySelect.Options)
                {
                    actuale.Add(element.Text);
                }
                return actuale;
            }

        }
    }
}
