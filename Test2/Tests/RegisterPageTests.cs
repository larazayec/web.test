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
            registerPage.Alert();
           
            Assert.IsTrue(registerPage.IsOpened);
        }       
    }
}
