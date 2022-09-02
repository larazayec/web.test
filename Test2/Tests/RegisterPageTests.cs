using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test2.Pages;

namespace Test2.Tests
{
    internal class RegisterPageTest : BaseTest
    {   
        [SetUp]
        public void OpenRegister()
        {
            OpenDriver();
            driver.Url = "https://localhost:5001/api/register/deleteAll";
            /*driver.Url = "https://localhost:5001/Register";
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.TitleContains("Login"));*/
        }
        /*[TearDown]
        public void DelAllUsers()
        {
            OpenDriver();
            driver.Url = "https://localhost:5001/api/register/deleteAll";
        }*/

        [TestCase("test", "test@test.com", "newyork1", "newyork1")]
        public void RegisterPositive(string login, string email, string password, string confirmpassword)
        {
            RegisterPage registerPage = new RegisterPage(driver);
            registerPage.Open();
           
            registerPage.Registration(login, email, password, confirmpassword);
            registerPage.RegisterButton.Click();
            registerPage.Alert();
           
            Assert.IsTrue(registerPage.IsOpened);
        }

        [TestCase(" ", "test@test.com", "newyork1", "newyork1", "Incorrect login!")]
        public void RegisterIncorrectLogin(string login, string email, string password, string confirmpassword, string expected)
        {
            RegisterPage registerPage = new RegisterPage(driver);
            registerPage.Open();

            registerPage.Registration(login, email, password, confirmpassword);
            registerPage.RegisterButton.Click();
            //Assert.IsFalse(registerPage.RegisterButton.Enabled);
            Assert.AreEqual(expected, registerPage.Error);

            string ActualUrl = driver.Url;
            string expectedUrl = "https://localhost:5001/Register";
            Assert.AreEqual(expectedUrl, ActualUrl);
        }

        [TestCase("test", "testtest.com", "newyork1", "newyork1", "Invalid email.")]
        public void RegisterIncorrectEmail(string login, string email, string password, string confirmpassword, string expected)
        {
            RegisterPage registerPage = new RegisterPage(driver);
            registerPage.Open();

            registerPage.Registration(login, email, password, confirmpassword);
            registerPage.RegisterButton.Click();
            //Assert.IsFalse(registerPage.RegisterButton.Enabled);
            Assert.AreEqual(expected, registerPage.Error);

            string ActualUrl = driver.Url;
            string expectedUrl = "https://localhost:5001/Register";
            Assert.AreEqual(expectedUrl, ActualUrl);
        }

        [TestCase("test", " ", "newyork1", "newyork1", "Invalid email.")]
        public void RegisterEmailEmpty(string login, string email, string password, string confirmpassword, string expected)
        {
            RegisterPage registerPage = new RegisterPage(driver);
            registerPage.Open();

            registerPage.Registration(login, email, password, confirmpassword);
            registerPage.RegisterButton.Click();
            //Assert.IsFalse(registerPage.RegisterButton.Enabled);
            Assert.AreEqual(expected, registerPage.Error);

            string ActualUrl = driver.Url;
            string expectedUrl = "https://localhost:5001/Register";
            Assert.AreEqual(expectedUrl, ActualUrl);
        }

        [TestCase("test", "test@test", "newyork1", "newyork1", "Invalid email.")]
        public void RegisterIncorrectEmailDomen(string login, string email, string password, string confirmpassword, string expected)
        {
            RegisterPage registerPage = new RegisterPage(driver);
            registerPage.Open();

            registerPage.Registration(login, email, password, confirmpassword);
            registerPage.RegisterButton.Click();
            //Assert.IsFalse(registerPage.RegisterButton.Enabled);
            Assert.AreEqual(expected, registerPage.Error);

            string ActualUrl = driver.Url;
            string expectedUrl = "https://localhost:5001/Register";
            Assert.AreEqual(expectedUrl, ActualUrl);
        }
    }


}
