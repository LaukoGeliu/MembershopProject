using NUnit.Framework;
using OpenQA.Selenium;

namespace MembershopTest.Page
{
    public class MembershopCartPage : BasePage
    {
        private IWebElement _selectedSizeInCart => Driver.FindElement(By.CssSelector(".article-attributes"));
        private IWebElement _itemPrice => Driver.FindElement(By.XPath("//div[@id='checkoutBasket']//td[@class='price']"));
        public MembershopCartPage(IWebDriver webdriver) : base(webdriver) 
        { }


        public void VerifySelectedSizeInCart(string verifySize)
        {
            Assert.IsTrue(_selectedSizeInCart.Text.Contains(verifySize), $"Selected amount should be {verifySize}, but is {_selectedSizeInCart.Text}");
        }

        public void VerifySelectedItemPriceInCart(string price)
        {
            Assert.IsTrue(_itemPrice.Text.Split('&')[0].Contains(price), $"Selected amount should be {price}, but is {_itemPrice.Text}");
        }
    }
}
