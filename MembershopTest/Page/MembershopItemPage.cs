using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace MembershopTest.Page
{
    public class MembershopItemPage : BasePage
    {

        private IReadOnlyCollection<IWebElement> _sizeButtons => Driver.FindElements(By.XPath("//div[@class='variants clearfix']//*[@type='button']"));
        private IWebElement _addIntoCartVButtonSubmit => Driver.FindElement(By.XPath("//div[@class='variant-select']//input[@type='submit'][@value='Pirkti']"));
        private IWebElement _addIntoCartVButtonButton => Driver.FindElement(By.XPath("//div[@class='variant-select']//input[@type='button'][@value='Pirkti']"));

        private IWebElement _cartIconTop => Driver.FindElement(By.CssSelector(".basket.navbar-item.dropdown.active"));
        private IWebElement _cartIconPopUp => Driver.FindElement(By.XPath("//a[@class='btn btn-primary btn-block btn-checkout gaq-track-event-click'][@data-ga_label='Pirkti iš karto']"));



        public MembershopItemPage(IWebDriver webdriver) : base(webdriver)
        { }

        public void SelectSize(string size)
        {
            foreach (IWebElement sizeOption in _sizeButtons)
            {
                if (sizeOption.Text.Contains(size))
                {
                    sizeOption.Click();
                    break;
                }
            }

        }

        public void AddIntoCart()
        {
            if (_addIntoCartVButtonSubmit.Enabled)
            { 
                _addIntoCartVButtonSubmit.Click(); 
            }
            if (!(_addIntoCartVButtonSubmit.Enabled))
            {
                _addIntoCartVButtonButton.Click();
            }
        }

        public void OpenCartImmediately()
        {
            Actions action = new Actions(Driver);
            action.MoveToElement(_cartIconTop);
            GetWait().Until(d => _cartIconPopUp.Displayed);
            _cartIconPopUp.Click();
        }

    }
}


