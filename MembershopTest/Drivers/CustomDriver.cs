using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;
using System;

namespace MembershopTest.Drivers
{
    public class CustomDriver
    {
        public static IWebDriver GetChromeDriver()
        {
            return GetDriver(Browsers.Chrome);
        }

        public static IWebDriver GetFirefoxDriver()
        {
            return GetDriver(Browsers.Firefox);
        }

        public static IWebDriver GetOperaDriver()
        {
            return GetDriver(Browsers.Opera);
        }

        public static IWebDriver GetChromeWithOptions()
        {
            return GetDriver(Browsers.ChromeWithoutNotifications);
        }
        private static IWebDriver GetDriver(Browsers browserName)
        {
            IWebDriver driver = null;

            switch (browserName)
            {
                case Browsers.Firefox:
                    driver = new FirefoxDriver();
                    break;
                case Browsers.Chrome:
                    driver = new ChromeDriver();
                    break;
                case Browsers.Opera:
                    driver = new OperaDriver();
                    break;
                case Browsers.ChromeWithoutNotifications:
                    driver = GetChromeWithoutNotifications();
                    break;
            }

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            return driver;
        }
        private static IWebDriver GetChromeWithoutNotifications()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-notifications");
            return new ChromeDriver(options);
        }
    }
}
