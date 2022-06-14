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
        public IWebElement SaveButton => driver.FindElement(By.Id("save"));
        public IWebElement LogoutButton => driver.FindElement(By.XPath("//div[contains(@class, 'login')]"));
        public IWebElement simb => driver.FindElement(By.Id("currency"));

        private void WaitForReady()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                wait.Until(ExpectedConditions.ElementToBeClickable(SaveButton));
            }
            catch (Exception ex)
            {
            }
        }

        private void Alert()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
             .Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }

        public void SetDateFormat(string format)
        {
            SelectElement dateFormSeletct = new SelectElement(DateFormat);
            dateFormSeletct.SelectByText(format);
            WaitForReady();
            SaveButton.Click();
            Alert();
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

        public void SetCurrency(string curren)
        {
            SelectElement currencySelect = new SelectElement(DefaultCurrency);
            currencySelect.SelectByText(curren);
            WaitForReady();
            SaveButton.Click();
            Alert();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(ExpectedConditions.UrlContains("Calculator"));
        }

        public void Logout()
        {
            LogoutButton.Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(ExpectedConditions.TitleContains("Login"));
        }
        public void SaveNumberFormat(string format)
        {
            SelectElement namberSelect = new SelectElement(NumberFormat);
            namberSelect.SelectByText(format);
            WaitForReady();
            SaveButton.Click();
            Alert();
        }

        public void Open()
        {
            driver.Url = "https://localhost:5001/Settings";
        }
    }
}
