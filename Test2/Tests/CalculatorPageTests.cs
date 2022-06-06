﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Threading;
using Test2.Pages;

namespace Test2.Tests
{
    internal class CalculatorPageTests
    {
        public IWebDriver driver;

        public object CalculateButton { get; private set; }

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
        private void WaitForReady()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("calculateBtn")));
        }

        [TestCase("100", "100", "365", "01", "January", "2022")]
        public void SecondPageEndDate(string deposit, string interest, string termin, string day, string month, string year)
        {
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            calculatorPage.Calculator(deposit, interest, termin, day, month, year);
            WaitForReady();
            IWebElement endDay = driver.FindElement(By.Id("endDate"));
            Assert.AreEqual("01/01/2023", endDay.GetAttribute("value"));
        }

        [Test]

        public void VerifMonth()
        {
            List<string> actuale = new List<string>();
            List<string> expected = new List<string>() { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            IWebElement startMonth = driver.FindElement(By.Id("month"));
            SelectElement startMonthSeletct = new SelectElement(startMonth);
            foreach (IWebElement element in startMonthSeletct.Options)
            {
                actuale.Add(element.Text);
            }
            Assert.AreEqual(expected, actuale);
        }

        [Test]

        public void VarifY()
        {
            List<string> actuale = new List<string>();
            List<string> expected = new List<string>();
            IWebElement startYear = driver.FindElement(By.Id("year"));
            SelectElement startYearSelect = new SelectElement(startYear);
            for (int i = 2010; i < 2030; i++)
            {
                expected.Add(i.ToString());
            }
            foreach (IWebElement element in startYearSelect.Options)
            {
                actuale.Add(element.Text);
            }
            Assert.AreEqual(expected, actuale);
        }

        [TestCase("100000", "50", "365", "01", "January", "2022")]

        public void FinfncialYearPos(string deposit, string interest, string termin, string day, string month, string year)
        {
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            calculatorPage.Calculator(deposit, interest, termin, day, month, year);
            IWebElement income = driver.FindElement(By.Id("income"));
            string expected = "150,000.00";
            Assert.AreEqual(expected, income.GetAttribute("value"));
        }

        [Test]

        public void FinancialTermN()
        {
            IWebElement depAm = driver.FindElement(By.Id("amount"));
            IWebElement rateInt = driver.FindElement(By.Id("percent"));
            IWebElement term = driver.FindElement(By.Id("term"));
            depAm.SendKeys("100000");
            rateInt.SendKeys("50");
            term.SendKeys("366");
            IWebElement term1 = driver.FindElement(By.Id("term"));
            string expected = "0";
            Assert.AreEqual(expected, term1.GetAttribute("value"));
            IWebElement butCalc = driver.FindElement(By.Id("calculateBtn"));
            Assert.IsFalse(butCalc.Enabled); // кращщий варіант - перевірка що кнопка не активна
        }

        [TestCase("100000", "101", "365", "01", "January", "2022")]

        public void InterestN(string deposit, string interest, string termin, string day, string month, string year)
        {
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            calculatorPage.Calculator(deposit, interest, termin, day, month, year);
            /*IWebElement depAm = driver.FindElement(By.Id("amount"));
            IWebElement rateInt = driver.FindElement(By.Id("percent"));
            IWebElement term = driver.FindElement(By.Id("term"));
            IWebElement startDay = driver.FindElement(By.Id("day"));
            IWebElement startMonth = driver.FindElement(By.Id("month"));
            IWebElement startYear = driver.FindElement(By.Id("year"));
            IWebElement calcBut = driver.FindElement(By.Id("calculateBtn"));
            IWebElement termBut = driver.FindElement(By.XPath("(//input[@type='radio'])[2]"));*/
            Thread.Sleep(600);
            IWebElement rateInt1 = driver.FindElement(By.Id("percent"));
            string expected = "0";
            Assert.IsFalse(CalculateButton.Enabled);
            Assert.AreEqual(expected, rateInt1.GetAttribute("value"));
        }

        [Test]
        public void NamesLabel()
        {
            IWebElement depAm = driver.FindElement(By.XPath("(*//td[contains ( text(), 'Deposit' )])"));
            IWebElement rateInt = driver.FindElement(By.XPath("//input[@id='percent']/../../td[1]"));
            IWebElement term = driver.FindElement(By.XPath("//input[@id='term']/../../td[1]"));
            IWebElement startDate = driver.FindElement(By.XPath("//select[@id='day']/../../td[1]"));
            IWebElement finansYear = driver.FindElement(By.XPath("//input[@type='radio']/../../td[1]"));
            IWebElement enterEar = driver.FindElement(By.XPath("//input[@id='interest']/../../th[1]"));
            IWebElement incom = driver.FindElement(By.XPath("//input[@id='income']/../../th[1]"));
            IWebElement endDay = driver.FindElement(By.XPath("//input[@id='endDate']/../../th[1]"));
            Assert.Multiple(() => {
                Assert.AreEqual("Deposit amount: *", depAm.Text);
                Assert.AreEqual("Interest rate: *", rateInt.Text);
                Assert.AreEqual("Investment Term: *", term.Text);
                Assert.AreEqual("Start Date: *", startDate.Text);
                Assert.AreEqual("Financial Year: *", finansYear.Text);
                Assert.AreEqual("Interest Earned: *", enterEar.Text);
                Assert.AreEqual("Income: *", incom.Text);
                Assert.AreEqual("End Date: *", endDay.Text);
            });
        }

        [Test]
        public void TermMore360()
        {
            IWebElement depAm = driver.FindElement(By.Id("amount"));
            IWebElement rateInt = driver.FindElement(By.Id("percent"));
            IWebElement term = driver.FindElement(By.Id("term"));
            IWebElement startDay = driver.FindElement(By.Id("day"));
            IWebElement startMonth = driver.FindElement(By.Id("month"));
            IWebElement startYear = driver.FindElement(By.Id("year"));
            IWebElement termBut1 = driver.FindElement(By.XPath("//*[contains ( text (), '365 days')]/./input"));
            IWebElement termBut2 = driver.FindElement(By.XPath("//*[contains ( text (), '360 days')]/./input"));
            depAm.SendKeys("100");
            rateInt.SendKeys("100");
            term.SendKeys("361");
            SelectElement startDaySelect = new SelectElement(startDay);
            startDaySelect.SelectByText("1");
            startMonth.SendKeys("January");
            startYear.SendKeys("2022");
            Assert.IsTrue(termBut1.Enabled);
            Assert.IsFalse(termBut2.Enabled);

        }
    }

}




