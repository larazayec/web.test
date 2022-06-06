using OpenQA.Selenium;
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
        public IWebElement FinancialYearButton => driver.FindElement(By.XPath("//input[@type='radio']"));
        public IWebElement IncomeField => driver.FindElement(By.XPath("//th[text()='Income: *']/..//input"));
        public IWebElement InterestEarnedField => driver.FindElement(By.XPath("//th[text()='Interest Earned: *']/..//input"));
        public IWebElement EndDateField => driver.FindElement(By.XPath("//th[text()='End Date: *']/..//input"));
        public void Calculator(string deposit, string interest, string termin, string day, string month, string year)
        {
            DepositField.SendKeys(deposit);
            InterestField.SendKeys(interest);
            TerminField.SendKeys(termin);
            DayField.Click();
            MonthField.SendKeys(month);
            YearField.SendKeys(year);
            FinancialYearButton.Click();
            CalculateButton.Click();

        }

    }
}


