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

        public int HistoryCount => TableValues.Count() - 1;
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

        public List<List<string>> TableValues
        { 
            get
            {
                List<List<string>> resultValues = new List<List<string>>();
                List<IWebElement> Rows = driver.FindElements(By.XPath("//tr")).ToList();

                foreach (IWebElement row in Rows)
                {
                    List<string> RowValues = new List<string>();
                    List<IWebElement> cells = row.FindElements(By.XPath(".//*")).ToList();

                    foreach (IWebElement cell in cells)
                    {
                        RowValues.Add(cell.Text);
                    }

                    resultValues.Add(RowValues);

                }

                return resultValues;
            }
        }
    }
}
