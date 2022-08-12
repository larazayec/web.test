using NUnit.Framework;
using System.Collections.Generic;
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
        }

        [Test]
        public void LastHistory()
        {
            HistoryPage historyPage = new HistoryPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            List<string> expected = new List<string>() {"100,000.00", "100%", "365", "365", "01/01/2022", "01/01/2023", "100,000.00", "200,000.00" };

            calculatorPage.Open();
            calculatorPage.Calculate("100000", "100", "365", "01", "January", "2022");
            historyPage.Open();
           
            Assert.AreEqual(expected, historyPage.TableValues[1]);
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

            Assert.AreEqual(0, historyPage.HistoryCount);
        }

        [Test]
        public void NumberRow()
        {
            HistoryPage historyPage = new HistoryPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);

            historyPage.Open();
            historyPage.ClearButton.Click();
            calculatorPage.Open();

            for (int i = 0; i < 10; i++)
            {
                calculatorPage.Calculate("100000", "100", "365", "01", "January", "2022");
            }
            historyPage.Open();
           
            Assert.AreEqual(10, historyPage.HistoryCount);
        }

        [Test]
        public void VerifyHistoryTableTest()
        {
            new HistoryPage(driver).Open();
            HistoryPage historyPage = new HistoryPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            List<List<string>> expected = new List<List<string>>()
            {
                new List<string>() { "Amount", "%", "Term", "Year", "From", "To", "Interest", "Income" },
                new List<string>() { "1,000.00", "50%", "360", "365", "01/01/2022", "27/12/2022", "493.15", "1,493.15" },
                new List<string>() { "1,000.00", "50%", "360", "365", "01/01/2022", "27/12/2022", "493.15", "1,493.15" },
                new List<string>() { "1,000.00", "50%", "360", "365", "01/01/2022", "27/12/2022", "493.15", "1,493.15" },
                new List<string>() { "1,000.00", "50%", "360", "365", "01/01/2022", "27/12/2022", "493.15", "1,493.15" },
                new List<string>() { "1,000.00", "50%", "360", "365", "01/01/2022", "27/12/2022", "493.15", "1,493.15" },
                new List<string>() { "200.00", "100%", "365", "365", "01/01/2022", "01/01/2023", "200.00", "400.00" },
                new List<string>() { "200.00", "100%", "365", "365", "01/01/2022", "01/01/2023", "200.00", "400.00" },
                new List<string>() { "200.00", "100%", "365", "365", "01/01/2022", "01/01/2023", "200.00", "400.00" },
                new List<string>() { "200.00", "100%", "365", "365", "01/01/2022", "01/01/2023", "200.00", "400.00" },
                new List<string>() { "200.00", "100%", "365", "365", "01/01/2022", "01/01/2023", "200.00", "400.00" },
            };

            historyPage.ClearButton.Click();
            calculatorPage.Open();
            for (int i = 0; i < 5; i++)
            {
                calculatorPage.Calculate("200", "100", "365", "01", "January", "2022");
            }
            for (int i = 0; i < 5; i++)
            {
                calculatorPage.Calculate("1000", "50", "360", "01", "January", "2022");
            }
            historyPage.Open();

            Assert.AreEqual(expected, historyPage.TableValues);
        }
    }
}
