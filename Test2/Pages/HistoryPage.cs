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
        public List<IWebElement> LastResult => driver.FindElements(By.XPath("//table/tr[@class='data-td'][1]/td")).ToList();
        public IWebElement ClearButton => driver.FindElement(By.Id("clear"));
        public void Open()
        {
            driver.Url = "https://localhost:5001/History";
        }

        public List<string> LastResults
        {
            get
            {
                List<string> actuale = new List<string>();
                foreach (IWebElement element in LastResult)
                {
                    actuale.Add(element.Text);
                }
                
               /* for (int i = 6; i <= 7; i++)
                {
                    actuale.Add(historySelect.Options[i].Text);
                }*/
                return actuale;
            }

        }
    }
}
