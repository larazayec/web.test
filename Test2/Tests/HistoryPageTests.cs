using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            calculatorPage.Open();
            calculatorPage.Calculate("100000", "100", "365", "01", "January", "2022");
            historyPage.Open();
            List<string> expected = new List<string>() {"100,000.00", "100%", "365", "365", "01/01/2022", "01/01/2023", "100,000.00", "200,000.00"};
            Assert.AreEqual(expected, historyPage.LastResults);

            List<List <string>> expected2 = new List<List<string>>()
            {
                new List<string>{"100,000.00", "100%", "365", "365", "01/01/2022", "01/01/2023", "100,000.00", "200,000.00" },
                new List<string>{"100,000.00", "100%", "365", "365", "01/01/2022", "01/01/2023", "100,000.00", "200,000.00" },
            };
        }
        
        [Test]
        public void ClearHistory()
        {
            HistoryPage historyPage = new HistoryPage(driver);
            historyPage.ClearButton.Click();
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            calculatorPage.Open();
            historyPage.HistoryButton.Click();
            int size = historyPage.LastResult.Count;
            Assert.AreEqual(0, size);
        }

        [Test]
        public void NumberResults()
        {
            HistoryPage historyPage = new HistoryPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            historyPage.ClearButton.Click();
            for (int i=0; i<10; i++)
                {
                    calculatorPage.Open();
                    calculatorPage.Calculate("100000", "100", "365", "01", "January", "2022");
                }
            

           

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
            int size = historyPage.AllResult.Count;
            Assert.AreEqual(10, size);

        }
    }
}
