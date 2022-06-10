using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using Test2.Pages;

namespace Test2.Tests
{
    internal class CalculatorPageTests
    {
        public IWebDriver driver;

        [OneTimeSetUp]
        public void ResetSetingsToDefault()
        {
            SetUp();
            driver.Url = "https://localhost:5001/Settings";
            IWebElement dateForm = driver.FindElement(By.XPath("//select[@id='dateFormat']"));
            SelectElement dateFormSeletct = new SelectElement(dateForm);
            dateFormSeletct.SelectByText("dd/MM/yyyy");
            IWebElement number = driver.FindElement(By.XPath(".//select[@id='numberFormat']"));
            IWebElement btnSave = driver.FindElement(By.Id("save"));
            SelectElement numberSelect = new SelectElement(number);
            numberSelect.SelectByText("123,456,789.00");
            btnSave.Click();
            TearDown();
        }

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "https://localhost:5001/Calculator";
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [TestCase("100", "100", "365", "01", "January", "2022")]
        public void SecondPageEndDate(string deposit, string interest, string termin, string day, string month, string year)
        {
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            calculatorPage.Calculate(deposit, interest, termin, day, month, year);
            Assert.AreEqual("01/01/2023", calculatorPage.EndDay);
        }

        [Test]
        public void VerifMonth()
        {
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            List<string> expected = new List<string>() { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            Assert.AreEqual(expected, calculatorPage.Months);
        }

        [Test]
        public void VarifY()
        {
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            List<string> expected = new List<string>();
            for (int i = 2010; i < 2030; i++)
            {
                expected.Add(i.ToString());
            }
            Assert.AreEqual(expected, calculatorPage.Years);
        }

        [TestCase("100000", "50", "365", "01", "January", "2022")]
        public void FinancialYearPos(string deposit, string interest, string termin, string day, string month, string year)
        {
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            calculatorPage.Calculate(deposit, interest, termin, day, month, year);
            string expected = "150,000.00";
            Assert.AreEqual(expected, calculatorPage.Income);
            string expectedInterest = "50,000.00";
            Assert.AreEqual(expectedInterest, calculatorPage.InterestEarned);
        }

        [TestCase("100000", "50", "366", "01", "January", "2022")]

        public void FinancialTermN(string deposit, string interest, string termin, string day, string month, string year)
        {
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            calculatorPage.Calculate(deposit, interest, termin, day, month, year);
            string expected = "0";
            Assert.AreEqual(expected, calculatorPage.TerminField.GetAttribute("value"));
            Assert.IsFalse(calculatorPage.CalculateButton.Enabled); // кращщий варіант - перевірка що кнопка не активна
        }

        [TestCase("100000", "101", "365", "01", "January", "2022")]
        public void InterestN(string deposit, string interest, string termin, string day, string month, string year)
        {
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            calculatorPage.Calculate(deposit, interest, termin, day, month, year);
            string expected = "0";
            Assert.IsFalse(calculatorPage.CalculateButton.Enabled);
            Assert.AreEqual(expected, calculatorPage.Interest);
        }

        [TestCase("100", "100", "361", "01", "January", "2022")]
        public void TermMore360(string deposit, string interest, string termin, string day, string month, string year)
        {
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            calculatorPage.Calculate(deposit, interest, termin, day, month, year);
            Assert.IsTrue(calculatorPage.FinancialYearButton1.Enabled);
            Assert.IsFalse(calculatorPage.FinancialYearButton2.Enabled);
        }
    }
}




