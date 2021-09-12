using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MembershopTest.Page
{
    public class BasePage
    {
        protected static IWebDriver Driver;
        public BasePage(IWebDriver webDriver)
        {
            Driver = webDriver;
        }

        public WebDriverWait GetWait(int seconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));
            return wait;
        }

        public void CloseBrowser()
        {
            Driver.Quit();
        }
    }
}
