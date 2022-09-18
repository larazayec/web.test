using NUnit.Framework;
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
            driver.Url = "https://localhost:5001/";
        }

        [Test]
        public void Remind()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.RemindPasswordButton.Click();
        }
    }
}
