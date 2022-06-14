using NUnit.Framework;
using Test2.Pages;

namespace Test2.Tests
{
    internal class LoginPageTests : BaseTest
    {
        [SetUp]
        public void OpenLogin()
        {
            OpenDriver();
            driver.Url = "https://localhost:5001/";
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