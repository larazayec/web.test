using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Test2.Pages;

namespace Test2.Tests
{
    internal class BaseTest
    {
        public IWebDriver driver;

        public void OpenDriver()
        {
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CreateTestUser();
            new LoginPage(driver).Open();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        public void CreateTestUser()
        {
            RegisterPage registerPage = new RegisterPage(driver);
            registerPage.Open();
            registerPage.Registration("test", "test@test.com", "newyork1", "newyork1");
        }
    }
}
