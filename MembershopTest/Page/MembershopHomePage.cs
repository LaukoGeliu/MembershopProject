using OpenQA.Selenium;
using System.Collections.Generic;
using System;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;
using System.Linq;

namespace MembershopTest.Page
{
    public class MembershopHomePage : BasePage
    {
        private const string PageAddress = "https://membershop.lt/#";
        private IWebElement _searchField => Driver.FindElement(By.ClassName("sn-suggest-input"));

        private IWebElement _loginDirection => Driver.FindElement(By.ClassName("not-logged-login"));
        private IWebElement _loginPopUp => Driver.FindElement(By.ClassName("modal-form"));
        private IWebElement _loginPopUpFieldCloseButton => Driver.FindElement(By.CssSelector("#loginModal>div>div>button"));
        private IWebElement _emailInsertField => Driver.FindElement(By.Id("LoginEmail"));
        private IWebElement _passwordInsertField => Driver.FindElement(By.Id("LoginPwd"));
        private IWebElement _loginButton => Driver.FindElement(By.Id("Login"));
        private IWebElement _wrongLoginMessage => Driver.FindElement(By.ClassName("errorLine"));
        private IWebElement _userAccountNavigation => Driver.FindElement(By.XPath("//div[@id='dropdownMenu1']/a"));
        private IWebElement _correctLoginMessage => Driver.FindElement(By.XPath("//div[@id='dropdownMenu1']/a"));
        private IWebElement _logoutButton => Driver.FindElement(By.LinkText("Atsijungti"));

        private IReadOnlyCollection<IWebElement> _discountCategoryFields => Driver.FindElements(By.XPath("//*[@class='item col-md-4 col-sm-6 col-xs-12']//*[@data-sale_title]"));
        private IReadOnlyCollection<IWebElement> _targetCategoryField => Driver.FindElements(By.XPath("//*[@class='dropdown-mobile-menu--not-active']"));
        private IReadOnlyCollection<IWebElement> _itemsGroupByTarget => Driver.FindElements(By.XPath("//div[@class='sale']"));
        public MembershopHomePage(IWebDriver webdriver) : base(webdriver) 
        { }

        public void NavigateToPage()
        {
            if (Driver.Url != PageAddress)
                Driver.Url = PageAddress;
        }

        public void AcceptCookies()
        {
            Cookie myCookie = new Cookie("iCookiePermissionLevel",
                "0", ".membershop.lt",
                "/",
                DateTime.Now.AddDays(5));
            Driver.Manage().Cookies.AddCookie(myCookie);
            Driver.Navigate().Refresh();
        }
        public void SelectLogIn()
        {
            _loginDirection.Click();
        }

        public void InsertUserLoginData(string login, string password)
        {
            GetWait().Until(d => _loginPopUp.Displayed);
            _emailInsertField.SendKeys(login);
            _passwordInsertField.SendKeys(password);
        }

        public void ClickLoginButton()
        {
            _loginButton.Click();
        }

        public void VerifyLoginResult(bool shouldBeLogged, string resultLoginMessage)
        {
            if (shouldBeLogged == false)
            {
                GetWait().Until(d => _loginPopUp.Displayed);
                Assert.AreEqual(resultLoginMessage, _wrongLoginMessage.Text, $"Message for wrong Login data should be {resultLoginMessage}, but is {_wrongLoginMessage}");
                _loginPopUpFieldCloseButton.Click();
            }

            else
            {
                GetWait().Until(d => _userAccountNavigation.Displayed);
                Assert.AreEqual(resultLoginMessage, _correctLoginMessage.Text, $"Message for correct Login data should be {resultLoginMessage}, but is {_correctLoginMessage}");
                Actions action = new Actions(Driver);
                action.MoveToElement(_userAccountNavigation);
                action.Build().Perform();
                _logoutButton.Click();
            }
        }

        public void InsertSearchValue(string searchValue)
        {
            _searchField.SendKeys(searchValue);
            Actions action = new Actions(Driver);
            action.SendKeys(Keys.Enter);
            action.Build().Perform();
        }
        public void NavigateToAndOpenSaleCategory(string saleCategoryTitle)
        {
            foreach (IWebElement saleCategory in _discountCategoryFields)
            {
                if (saleCategory.GetAttribute("data-sale_title").Contains(saleCategoryTitle))
                {
                    saleCategory.Click();
                    break;
                }
            }
        }

        public void SelectTargetAntItemsGroup(int category, string itemsGroup)
        {
            Actions moveMouse = new Actions(Driver);
            moveMouse.MoveToElement(_targetCategoryField.ElementAt(0));
            moveMouse.Build().Perform();
            foreach (IWebElement group in _itemsGroupByTarget)
            {
                if (group.Text.Contains(itemsGroup))
                {
                    group.Click();
                    break;
                }
            }
        }

        public void LogOut()
        {
            Actions action = new Actions(Driver);
            action.MoveToElement(_userAccountNavigation);
            action.Build().Perform();
            _logoutButton.Click();
        }
    }
}





