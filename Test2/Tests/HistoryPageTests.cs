using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Test2.Pages;

namespace Test2.Tests
{
    internal class HistoryPageTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            OpenDriver();
            SettingPage settingPage = new SettingPage(driver);
            settingPage.Open();
            settingPage.SaveNumberFormat("123,456,789.00");
            driver.Url = "https://localhost:5001/History";
        }

        [Test]
        public void LastHistory()
        {
            HistoryPage historyPage = new HistoryPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            List<string> expected = new List<string>() { "100,000.00", "100%", "365", "365", "01/01/2022", "01/01/2023", "100,000.00", "200,000.00" };

            calculatorPage.Open();
            calculatorPage.Calculate("100000", "100", "365", "01", "January", "2022");
            historyPage.Open();
           
            Assert.AreEqual(expected, historyPage.LastResults);
        }
        
        [Test]
        public void ClearHistory()
        {
            HistoryPage historyPage = new HistoryPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);

            calculatorPage.Open();
            calculatorPage.Calculate("100000", "100", "365", "01", "January", "2022");
            historyPage.Open();
            historyPage.ClearButton.Click();

            Assert.AreEqual(0, historyPage.Rows.Count);
        }

        [Test]
        public void NumberRow()
        {
            HistoryPage historyPage = new HistoryPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);

            historyPage.ClearButton.Click();
            for (int i = 0; i < 10; i++)
            {
                calculatorPage.Open();
                calculatorPage.Calculate("100000", "100", "365", "01", "January", "2022");
            }
            historyPage.Open();
           
            Assert.AreEqual(10, historyPage.Rows.Count);
        }

        [Test]
        public void Lists()
        {
            new HistoryPage(driver).Open();

            List<List<string>> tabelValues = new List<List<string>>();
            List<IWebElement> Rows = driver.FindElements(By.XPath("//tr")).ToList();

            foreach (IWebElement row in Rows)
            {
                List<string> RowValues = new List<string>();
                List<IWebElement> cells = row.FindElements(By.XPath(".//td")).ToList();

                foreach (IWebElement cell in cells)
                {
                    RowValues.Add(cell.Text);
                }

                tabelValues.Add(RowValues);
            }
        }

        [Test]
        public void List2()
        {
            new HistoryPage(driver).Open();
            List<List<string>> tabelValues = new List<List<string>>();
            int RowsCount = driver.FindElements(By.XPath("//tr")).Count();

            for (int rowNumber = 0; rowNumber < RowsCount; rowNumber++)
            {
                List<string> rowValues = new List<string>();
                for (int colNumber = 0; colNumber < 8; colNumber++)
                {
                    IWebElement cell = driver.FindElement(By.XPath($"//tr[{rowNumber+1}]//*[{colNumber+1}]"));
                    string cellText = cell.Text;
                    rowValues.Add(cellText);
                }
                tabelValues.Add(rowValues);
            }
        }

        [Test]
        public void CheckElements()
        {
            new HistoryPage(driver).Open();
            HistoryPage historyPage = new HistoryPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            List<string> expected = new List<string>() { "200.00", "100%", "365", "365", "01/01/2022", "01/01/2023", "200.00", "400.00" };
            List<string> expected1 = new List<string>() { "1,000.00", "50%", "360", "365", "01/01/2022", "27/12/2022", "493.15", "1,493.15" };


            historyPage.ClearButton.Click();
            for (int i = 0; i < 5; i++)
            {
                calculatorPage.Open();
                calculatorPage.Calculate("200", "100", "365", "01", "January", "2022");
            }
            for (int i = 0; i < 5; i++)
            {
                calculatorPage.Open();
                calculatorPage.Calculate("1000", "50", "360", "01", "January", "2022");
            }
            historyPage.Open();

            List<List<string>> tabelValues = new List<List<string>>();
            List<IWebElement> Rows = driver.FindElements(By.XPath("//tr")).ToList();
           
            foreach (IWebElement row in Rows)
            {
                List<string> RowValues = new List<string>();
                List<IWebElement> cells = row.FindElements(By.XPath(".//td")).ToList();

                foreach (IWebElement cell in cells)
                {
                    RowValues.Add(cell.Text);
                }

                tabelValues.Add(RowValues);
               
            }
            Assert.AreEqual(expected1, tabelValues[1]);
            Assert.AreEqual(expected, tabelValues[9]);
        }
    }
}
