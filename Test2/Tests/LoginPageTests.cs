using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Test2.Pages;

namespace Test2.Tests
{
    internal class LoginPageTests
    {
        public IWebDriver driver;

        [SetUp]
        public void SetaUp()
        {
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "https://localhost:5001/";
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [TestCase("tes", "newyork1", "User not found!")]
        [TestCase("test", "newyork", "Incorrect password!")]
        public void NegativeTest(string login, string password, string expected)
        {
            LoginPage loginPage = new LoginPage(driver);

            loginPage.Login(login, password);

            Assert.AreEqual(expected, loginPage.Error);
        }

        [TestCase("test")]
        [TestCase("TEST")]
        public void PositiveLoginTest(string login)
        {
            LoginPage loginPage = new LoginPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);

            loginPage.Login(login, "newyork1");
            Assert.IsTrue(calculatorPage.IsOpened);
        }
    }
}