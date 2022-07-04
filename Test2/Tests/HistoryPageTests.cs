using NUnit.Framework;
using System.Collections.Generic;
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
    }
}
