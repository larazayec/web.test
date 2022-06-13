using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;

namespace Test2.Pages
{
    internal class SettingPage
    {
        private IWebDriver driver;
        public SettingPage(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        public IWebElement DateFormat => driver.FindElement(By.XPath("//th[text()='Date format:']/..//select[@id='dateFormat']"));
        public IWebElement DefaultCurrency => driver.FindElement(By.XPath("//th[text()='Default currency:']/..//select[@id='currency']"));
        public IWebElement NumberFormat => driver.FindElement(By.XPath("//th[text()='Number format:']/..//select[@id='numberFormat']"));
        public IWebElement ButtonSave => driver.FindElement(By.Id("save"));
        public IWebElement LogutButton => driver.FindElement(By.XPath("//div[contains(@class, 'login')]"));
        public IWebElement simb => driver.FindElement(By.Id("currency"));

        public string EndDay => driver.FindElement(By.XPath("//th[text()='End Date: *']/..//input")).GetAttribute("value");

        private void WaitForReady()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                wait.Until(ExpectedConditions.ElementToBeClickable(ButtonSave));
            }
            catch (Exception ex)
            {
            }
        }
        private void WaitForAlert()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
             .Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }
        public void Сonfigure(string format)
        {
            SelectElement dateFormSeletct = new SelectElement(DateFormat);
            dateFormSeletct.SelectByText(format);
            WaitForReady();
            ButtonSave.Click();
            WaitForAlert();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
             .Until(ExpectedConditions.TitleContains("Deposite"));
        }
        public List<string> Defaultcurrenc
        {
            get
            {
                List<string> actuale = new List<string>();
                SelectElement currencySeletct = new SelectElement(DefaultCurrency);
                foreach (IWebElement element in currencySeletct.Options)
                {
                    actuale.Add(element.Text);
                }
                return actuale;
            }
        }
        public void Currencysymbol(string curren, string simcur)
        {
            SelectElement currencySelect = new SelectElement(DefaultCurrency);
            currencySelect.SelectByText(curren);
            WaitForReady();
            ButtonSave.Click();
            WaitForAlert();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(ExpectedConditions.UrlContains("Calculator"));
        }
        public void Logut()
        {
            LogutButton.Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(ExpectedConditions.TitleContains("Login"));
        }
        public void NumbFormat(string format, string expectedincom, string expectedinterest)
        {
            SelectElement namberSelect = new SelectElement(NumberFormat);
            namberSelect.SelectByText(format);
            WaitForReady();
            ButtonSave.Click();
            WaitForAlert();
            IWebElement depAm = driver.FindElement(By.Id("amount"));
            IWebElement rateInt = driver.FindElement(By.Id("percent"));
            IWebElement term = driver.FindElement(By.Id("term"));
            IWebElement calcBut = driver.FindElement(By.Id("calculateBtn"));
            IWebElement termBut = driver.FindElement(By.XPath("//input[@type='radio']"));
            depAm.SendKeys("100000");
            rateInt.SendKeys("100");
            term.SendKeys("365");
            termBut.Click();
            calcBut.Click();
        }
        public string income => driver.FindElement(By.Id("income")).GetAttribute("value");
        public string interest => driver.FindElement(By.Id("interest")).GetAttribute("value");
    }
}
