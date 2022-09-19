using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test2.Pages;

namespace Test2.Tests
{
    internal class RemindPasswordPageTests : BaseTest
    {
        [SetUp]
        public void OpenLogin()
        {
            OpenDriver();
            driver.Url = "https://localhost:5001/Register";
            RegisterPage registerPage = new RegisterPage(driver);
            registerPage.Registration("lara", "lara.zayec@gmail.com", "123456", "123456");
            registerPage.RegisterButton.Click();
            registerPage.AcceptAlert();
        }

        [TestCase("lara.zayec@gmail.com", "The password has been sent to this email.")]
        public void Remind(string email, string expected)
        {
            LoginPage loginPage = new LoginPage(driver);
            RemindPasswordPage remindPassword = new RemindPasswordPage(driver);
            RegisterPage registerPage = new RegisterPage(driver);

            loginPage.RemindPasswordButton.Click();
            
            driver.SwitchTo().Frame(0);
            remindPassword.RemindPassword(email);
            remindPassword.AcceptAlert();
            Assert.AreEqual(expected, registerPage.Message);
        }
    }
}
