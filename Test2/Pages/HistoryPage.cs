using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public List<IWebElement> Rows => driver.FindElements(By.XPath("//table/tr[@class='data-td']")).ToList();
        public IWebElement ClearButton => driver.FindElement(By.Id("clear"));
        public IWebElement HistoryButton => driver.FindElement(By.XPath("//div[@class='history link btn btn-link']"));
        public void Open()
        {
            driver.Url = "https://localhost:5001/History";
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                wait.Until(ExpectedConditions.ElementExists(By.XPath("//table/tr[@class='data-td']")));
            }
            catch (Exception ex)
            {

            }
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
                
                return actuale;
            }
        }
    }
}
