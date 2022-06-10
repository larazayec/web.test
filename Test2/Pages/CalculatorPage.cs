using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;

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
        private IWebElement InterestField => driver.FindElement(By.XPath("//td[text()='Interest rate: *']/..//input"));
        public string Interest
        {
            get => InterestField.GetAttribute("value");
            set => InterestField.SendKeys(value);
        }
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
            Interest = interest;
            TerminField.SendKeys(termin);
            DayField.Click();
            MonthField.SendKeys(month);
            YearField.SendKeys(year);
            FinancialYearButton1.Click();
            WaitForReady();
            CalculateButton.Click();
            WaitForReady();
        }

        private void WaitForReady()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                wait.Until(ExpectedConditions.ElementToBeClickable(CalculateButton));
            } 
            catch (Exception ex)
            {

            }
        }

        public string EndDay => driver.FindElement(By.XPath("//th[text()='End Date: *']/..//input")).GetAttribute("value");

        public string Income
        {
            get
            {
                return driver.FindElement(By.XPath("//th[text()='Income: *']/..//input")).GetAttribute("value");
            }
        }

        public string InterestEarned => driver.FindElement(By.XPath("//th[text()='Interest Earned: *']/..//input")).GetAttribute("value");

        public List<string> Months
        {
            get
            {
                List<string> actuale = new List<string>();
                SelectElement startMonthSeletct = new SelectElement(MonthField);
                foreach (IWebElement element in startMonthSeletct.Options)
                {
                    actuale.Add(element.Text);
                }
                return actuale;
            }
        }
        public List<string> Years
        {
            get
            {
                List<string> actuale = new List<string>();
                SelectElement startYearSelect = new SelectElement(YearField);
                foreach (IWebElement element in startYearSelect.Options)
                {
                    actuale.Add(element.Text);
                }
                return actuale;
            }
        }
}
}


