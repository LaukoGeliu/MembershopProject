using NUnit.Framework;
using OpenQA.Selenium;

namespace MembershopTest.Page
{
    public class MembershopCartPage : BasePage
    {
        private IWebElement _selectedAmountInCart => Driver.FindElement(By.CssSelector(".form-control.input-sm.pull-left"));
        private IWebElement _selectedSizeInCart => Driver.FindElement(By.CssSelector(".article-attributes"));


        public MembershopCartPage(IWebDriver webdriver) : base(webdriver) 
        { }

        public void VerifySelectedAmountInCart(string verifyAmount)
        {
            Assert.AreEqual(verifyAmount, _selectedAmountInCart.GetAttribute("value"), $"Selected amount should be {verifyAmount}, but is {_selectedAmountInCart.GetAttribute("value")}");
        }

        public void VerifySelectedSizeInCart(string verifySize)
        {
            Assert.IsTrue(_selectedSizeInCart.Text.Contains(verifySize), $"Selected amount should be {verifySize}, but is {_selectedSizeInCart.Text}");
        }
    }
}
