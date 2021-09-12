
using MembershopTest.Drivers;
using MembershopTest.Page;
using MembershopTest.Tools;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MembershopTest.Test
{
    public class BaseTest
    {
        public static IWebDriver driver;
        public static MembershopHomePage _membershopHomePage;
        public static MembershopSearchResultPage _membershopSearchResultPage;
        public static MembershopItemPage _membershopItemPage;
        public static MembershopSaleCategoryPage _membershopSaleCategoryPage;
        public static MembershopCartPage _membershopCartPage;


        [OneTimeSetUp]
        public static void SetUp()
        {
            driver = CustomDriver.GetChromeWithOptions();

            _membershopHomePage = new MembershopHomePage(driver);
            _membershopSearchResultPage = new MembershopSearchResultPage(driver);
            _membershopItemPage = new MembershopItemPage(driver);
            _membershopSaleCategoryPage = new MembershopSaleCategoryPage(driver);
            _membershopCartPage = new MembershopCartPage(driver);
        }

        [TearDown]
        public static void TakeScreeshot()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                MyScreenshot.TakeScreenshot(driver);
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            driver.Quit();
        }

    }
}
