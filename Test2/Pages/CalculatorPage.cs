using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2.Pages
{
    internal class CalculatorPage
    {
        private IWebDriver driver;
        public CalculatorPage(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        public IWebElement DepositField => driver.FindElement(By.XPath("//td[text()='Deposit amount: *']/..//input"));
        public IWebElement InterestField => driver.FindElement(By.XPath("//td[text()='Interest rate: *']/..//input"));
        public IWebElement TerminField => driver.FindElement(By.XPath("//td[text()='Investment Term: *']/..//input"));
        public IWebElement DayField => driver.FindElement(By.XPath("//td[text()='Start Date: *']/..//option[@value='1']"));
        public IWebElement MonthField => driver.FindElement(By.XPath("//td[text()='Start Date: *']/..//select[@id='month']"));
        public IWebElement YearField => driver.FindElement(By.XPath("//td[text()='Start Date: *']/..//select[@id='year']"));
        public IWebElement CalculateButton => driver.FindElement(By.Id("calculateBtn"));
        public IWebElement FinancialYearButton1 => driver.FindElement(By.XPath("//*[contains ( text (), '365 days')]/./input"));
        public IWebElement FinancialYearButton2 => driver.FindElement(By.XPath("//*[contains ( text (), '360 days')]/./input"));

        public void Calculate(string deposit, string interest, string termin, string day, string month, string year)
        {
            DepositField.SendKeys(deposit);
            InterestField.SendKeys(interest);
            TerminField.SendKeys(termin);
            DayField.Click();
            MonthField.SendKeys(month);
            YearField.SendKeys(year);
            FinancialYearButton1.Click();
        }

        private void WaitForReady()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(CalculateButton));
            CalculateButton.Click();
        }

        public string EndDay
        {
            get
            {
                By locator = By.XPath("//th[text()='End Date: *']/..//input");
                new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(ExpectedConditions.ElementIsVisible(locator));
                return driver.FindElement(locator).Text;
            }
        }
        public string Income
        {
            get
            {
                By locator = By.XPath("//th[text()='Income: *']/..//input");
                new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(ExpectedConditions.ElementIsVisible(locator));
                return driver.FindElement(locator).Text;
            }
        }

        public string InterestEarned
        {
            get
            {
                By locator = By.XPath("//th[text()='Interest Earned: *']/..//input");
                new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(ExpectedConditions.ElementIsVisible(locator));
                return driver.FindElement(locator).Text;
            }
        }
    }
}


