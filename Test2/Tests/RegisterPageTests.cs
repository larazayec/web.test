using NUnit.Framework;
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
            driver.Url = "https://localhost:5001/Register";
        }

        [TestCase("test", "test@test.com", "newyork1", "newyork1")]
        public void RegisterPositive(string login, string email, string password, string cConfirmpassword)
        {
            RegisterPage registerPage = new RegisterPage(driver);
            CalculatorPage calculatorPage = new CalculatorPage(driver);

            registerPage.Registration(login, email, password, cConfirmpassword);
            Assert.IsTrue(calculatorPage.IsOpened);
        }       
    }
}
