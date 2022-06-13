using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using Test2.Pages;

namespace Test2.Tests
{
    internal class SettingPageTests
    {
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "https://localhost:5001/Settings";
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void CurrencyVer()
        {
            SettingPage settingPage = new SettingPage(driver);
            List<string> expected = new List<string>() { "$ - US dollar", "€ - euro", "£ - Great Britain Pound", "₴ - Ukrainian hryvnia"};
            Assert.AreEqual(expected, settingPage.Defaultcurrenc);
        }

        [TestCase("dd/MM/yyyy")]
        [TestCase("dd-MM-yyyy")]
        [TestCase("MM/dd/yyyy")]
        [TestCase("MM dd yyyy")]
        public void DateFormat(string format)
        {
            SettingPage settingPage = new SettingPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            settingPage.SetDateFormat(format);
            string expectedDate = DateTime.Now.ToString(format);
            Assert.AreEqual(expectedDate, calculatorPage.EndDay);
        }

        [TestCase("$ - US dollar", "$")]
        [TestCase("€ - euro", "€")]
        [TestCase("£ - Great Britain Pound", "£")]
        [TestCase("₴ - Ukrainian hryvnia", "₴")]
        public void Currency(string curren, string simcur)
        {
            SettingPage settingPage = new SettingPage(driver);
            settingPage.SetCurrency(curren);
            string ActualUrl = driver.Url;
            string expectedUrl = "https://localhost:5001/Calculator";
            Assert.AreEqual(expectedUrl, ActualUrl);
            Assert.AreEqual(simcur, settingPage.simb.Text);
        }

        [Test]
        public void Logut()
        {
            SettingPage settingPage = new SettingPage(driver);
            settingPage.Logout();
            Assert.AreEqual("https://localhost:5001/", driver.Url);
        }

        [TestCase("123,456,789.00", "200,000.00", "100,000.00")]
        [TestCase("123.456.789,00", "200.000,00", "100.000,00")]
        [TestCase("123 456 789.00", "200 000.00", "100 000.00")]
        [TestCase("123 456 789,00", "200 000,00", "100 000,00")]
        public void NumbForm(string format, string expectedincom, string expectedinterest)
        {
            SettingPage settingPage = new SettingPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);
            settingPage.NumbFormat(format, expectedincom, expectedinterest);
            Assert.AreEqual(expectedincom, calculatorPage.Income);
            Assert.AreEqual(expectedinterest, calculatorPage.InterestEarned);
        }
    }
}
