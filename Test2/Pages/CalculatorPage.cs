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
        public IWebElement DayField => driver.FindElement(By.XPath("//td[text()='Start Date: *']/..//select[@id='day']"));
        public IWebElement MonthField => driver.FindElement(By.XPath("//td[text()='Start Date: *']/..//select[@id='month']"));
        public IWebElement YearField => driver.FindElement(By.XPath("//td[text()='Start Date: *']/..//select[@id='year']"));
        public IWebElement Calculate => driver.FindElement(By.Id("calculateBtn"));


    }
}


/*IWebElement termBut = driver.FindElement(By.XPath("//input[@type='radio']"));
SelectElement startDaySelect = new SelectElement(startDay);*/
