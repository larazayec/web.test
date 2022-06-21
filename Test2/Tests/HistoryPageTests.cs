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
        public void ClearHistory()
        {
            HistoryPage historyPage = new HistoryPage(driver);
            IWebElement clearButton = driver.FindElement(By.Id("clear"));
            List<string> expected = new List<string>(7) { "Amount", "%", "Term", "Year", "From", "To", "Interest", "Income"};
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            Assert.AreEqual(expected, historyPage.HistoryFirst);


            /*IWebElement ferst = driver.FindElement(By.XPath("//table/tr/td[1]"));
            Assert.AreEqual("100.00", ferst.Text);
            IWebElement ferst2 = driver.FindElement(By.XPath("//table/tr/td[2]"));
            Assert.AreEqual("100%", ferst2.Text);
            //IWebDriver arrayNames = driver.FindElement(By.XPath("//tr[@class = 'data - th']"));
            clearButton.Click();*/

        }

    }
}
