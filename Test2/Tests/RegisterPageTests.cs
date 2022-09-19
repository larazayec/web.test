using NUnit.Framework;
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
        }

        [TestCase("test", "test@test.com", "newyork1", "newyork1")]
        public void RegisterPositive(string login, string email, string password, string confirmpassword)
        {
            RegisterPage registerPage = new RegisterPage(driver);
            LoginPage loginPage = new LoginPage(driver);
            registerPage.Open();
           
            registerPage.Registration(login, email, password, confirmpassword);
            registerPage.RegisterButton.Click();
            registerPage.AcceptAlert();
           
            Assert.IsTrue(loginPage.IsOpened);
        }

        [TestCase("test", "test@test.com", "newyork1", "newyork1", "User with this email is already registered.")]
        public void ReRegistration(string login, string email, string password, string confirmpassword, string expected)
        {
            RegisterPage registerPage = new RegisterPage(driver);
            LoginPage loginPage = new LoginPage(driver);
            registerPage.Open();

            registerPage.Registration(login, email, password, confirmpassword);
            registerPage.RegisterButton.Click();
            registerPage.AcceptAlert();

            Assert.IsTrue(loginPage.IsOpened);
            loginPage.RegisterButton.Click();
            registerPage.Registration(login, email, password, confirmpassword);
            registerPage.RegisterButton.Click();
            Assert.AreEqual(expected, registerPage.Message);

            string ActualUrl = driver.Url;
            string expectedUrl = "https://localhost:5001/Register";
            Assert.AreEqual(expectedUrl, ActualUrl);
        }

        [TestCase(" ", "test@test.com", "newyork1", "newyork1", "Incorrect login!")]
        [TestCase("test", "testtest.com", "newyork1", "newyork1", "Invalid email.")]
        [TestCase("test", "@@test.com", "newyork1", "newyork1", "Invalid email.")]
        [TestCase("test", " ", "newyork1", "newyork1", "Invalid email.")]
        [TestCase("test", "test@test", "newyork1", "newyork1", "Invalid email.")]
        [TestCase("test", "test@test.com", " ", " ", "Password is too short.")]
        [TestCase("test", "test@test.com", "1111", "1111", "Password is too short.")]
        [TestCase("test", "test@test.com", "newyork1", "newyork", "Passwords are different.")]
        [TestCase("test", "test@test.com", "new", "newyork1", "Passwords are different.")]
        [TestCase("test", "test@test.com", "12345678912345678", "12345678912345678", "Password from 5 to 16 characters.")]
        public void RegisterNegativeTests(string login, string email, string password, string confirmpassword, string expected)
        {
            RegisterPage registerPage = new RegisterPage(driver);
            registerPage.Open();

            registerPage.Registration(login, email, password, confirmpassword);
            registerPage.RegisterButton.Click();
            Assert.AreEqual(expected, registerPage.Message);

            string ActualUrl = driver.Url;
            string expectedUrl = "https://localhost:5001/Register";
            Assert.AreEqual(expectedUrl, ActualUrl);
        }
    }
}
