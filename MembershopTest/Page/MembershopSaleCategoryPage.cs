using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace MembershopTest.Page
{
    public class MembershopSaleCategoryPage : BasePage
    {
        private IWebElement _saleCategoryTitle => Driver.FindElement(By.XPath("//div[@class='sale-title']"));
        private IReadOnlyCollection<IWebElement> _targetGroupFields => Driver.FindElements(By.XPath("//*[@class='sale-block hidden-xs hidden-sm']//a[@class='behat-category']"));

        public MembershopSaleCategoryPage(IWebDriver webdriver) : base(webdriver) { }

        public void ValidateSaleTitle(string saleCategoryTitleValidation)
        {
            Assert.AreEqual(saleCategoryTitleValidation.ToLower(), _saleCategoryTitle.Text.ToLower(), $"Sale target is {_saleCategoryTitle.Text}, but should be {saleCategoryTitleValidation}");
        }

        public void SelectTargetGroup(string targetGroup)
        {
            foreach (IWebElement target in _targetGroupFields)
            {
                if (target.Text.ToLower().Contains(targetGroup.ToLower()))
                {
                    GetWait().Until(d => target.Enabled);
                    target.Click();
                    break;
                }
            }
        }
    }
}