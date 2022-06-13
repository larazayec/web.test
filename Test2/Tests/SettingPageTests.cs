using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
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
            settingPage.Сonfigure(format);
            string expectedDate = DateTime.Now.ToString(format);
            Assert.AreEqual(expectedDate, settingPage.EndDay);
        }

        [TestCase("$ - US dollar", "$")]
        [TestCase("€ - euro", "€")]
        [TestCase("£ - Great Britain Pound", "£")]
        [TestCase("₴ - Ukrainian hryvnia", "₴")]
        public void Currency(string curren, string simcur)
        {
            SettingPage settingPage = new SettingPage(driver);
            settingPage.Currencysymbol(curren, simcur);
            string ActualUrl = driver.Url;
            string expectedUrl = "https://localhost:5001/Calculator";
            Assert.AreEqual(expectedUrl, ActualUrl);
            Assert.AreEqual(simcur, settingPage.simb.Text);
        }


        [Test]
        public void Logut()
        {
            IWebElement logut = driver.FindElement(By.XPath("//div[contains(@class, 'login')]"));
            logut.Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(ExpectedConditions.TitleContains("Login"));
            string ActualUrl = driver.Url;
            string expectedUrl = "https://localhost:5001/";
            Assert.AreEqual(expectedUrl, ActualUrl);
        }

        [TestCase("123,456,789.00", "200,000.00", "100,000.00")]
        [TestCase("123.456.789,00", "200.000,00", "100.000,00")]
        [TestCase("123 456 789.00", "200 000.00", "100 000.00")]
        [TestCase("123 456 789,00", "200 000,00", "100 000,00")]
        public void NumbForm(string format, string expectedincom, string expectedinterest)
        {
            IWebElement namber = driver.FindElement(By.XPath(".//select[@id='numberFormat']"));
            IWebElement btnSave = driver.FindElement(By.Id("save"));
            SelectElement namberSelect = new SelectElement(namber);
            namberSelect.SelectByText(format);
            //WaitForReady();
            btnSave.Click();
           // WaitForAlert();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            IWebElement depAm = driver.FindElement(By.Id("amount"));
            IWebElement rateInt = driver.FindElement(By.Id("percent"));
            IWebElement term = driver.FindElement(By.Id("term"));
            IWebElement calcBut = driver.FindElement(By.Id("calculateBtn"));
            IWebElement termBut = driver.FindElement(By.XPath("//input[@type='radio']"));
            depAm.SendKeys("100000");
            rateInt.SendKeys("100");
            term.SendKeys("365");
            termBut.Click();
            calcBut.Click();
            IWebElement income = driver.FindElement(By.Id("income"));
            IWebElement interest = driver.FindElement(By.Id("interest"));
            Assert.AreEqual(expectedincom, income.GetAttribute("value"));
            Assert.AreEqual(expectedinterest, interest.GetAttribute("value"));
        }
    }
}
