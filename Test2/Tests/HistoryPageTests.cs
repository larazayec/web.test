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


            /*IWebElement ferst = driver.FindElement(By.XPath("//table/tr/td[1]"));
            Assert.AreEqual("100.00", ferst.Text);
            IWebElement ferst2 = driver.FindElement(By.XPath("//table/tr/td[2]"));
            Assert.AreEqual("100%", ferst2.Text);
            //IWebDriver arrayNames = driver.FindElement(By.XPath("//tr[@class = 'data - th']"));
            clearButton.Click();*/

        }

    }
}
