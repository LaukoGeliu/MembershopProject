using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MembershopTest.Page
{
    public class MembershopSearchResultPage : BasePage
    {
        private IWebElement _searchResultField => Driver.FindElement(By.CssSelector(".sale-title"));
        private IWebElement _saleCategoryTitle => Driver.FindElement(By.ClassName("sale-title"));
        private IWebElement _targetSingle => Driver.FindElement(By.XPath("//div[@class='sales-side-target']"));

        private IReadOnlyCollection<IWebElement> _brandsCheckboxes => Driver.FindElements(By.XPath("//div[@data-sn-container='s_title']//div[@class='searchnode ']"));
        private IWebElement _deselectBrandsAllCheckboxesOption => Driver.FindElement(By.XPath("//*[@class='clear-filter'][@data-clear-filter='s_title']"));

        private IWebElement maxSlider => Driver.FindElement(By.XPath("//span[@id='sn-slider-max']"));
        private IWebElement maxPriceDefault => Driver.FindElement(By.XPath("//input[@class='priceTo currency-width-1']"));
        private IReadOnlyCollection<IWebElement> itemPriceFields => Driver.FindElements(By.XPath("//div[@class='text-center']/span//strong"));

        private IWebElement _sliderScale => Driver.FindElement(By.Id("sn-slider-scale"));
        private IReadOnlyCollection<IWebElement> _selectedTargetCheckboxes => Driver.FindElements(By.XPath("//div[@data-sn-container='sm_category_1']//*[@class='searchnode sn-active']"));
        private IReadOnlyCollection<IWebElement> _items => Driver.FindElements(By.XPath("//div[@class='article-title text-center']"));

        private IWebElement _amountInSearchResultTitle => Driver.FindElement(By.XPath("//div[@class='sale-title']//small"));

        public MembershopSearchResultPage(IWebDriver webdriver) : base(webdriver)
        { }

        public void VerifySearchResult(string searchValue)
        {
            GetWait().Until(driver => _searchResultField.Displayed);
            Assert.IsTrue(_searchResultField.Text.ToLower().Contains(searchValue.ToLower()), $"Search result should be {searchValue}, but is {_searchResultField}");
        }

        public void VerifyTargetGroupAndSaleCategory(string saleCategoryTitleValidation, bool singleTargetResult, string selectedTargetResult)
        {
            Assert.IsTrue(_saleCategoryTitle.Text.ToLower().Contains(saleCategoryTitleValidation.ToLower()), $"Sale target is {_saleCategoryTitle.Text}, but should be {saleCategoryTitleValidation}");
            if (singleTargetResult)
            { Assert.AreEqual(selectedTargetResult.ToLower(), _targetSingle.Text.ToLower(), $"Sale target is {_targetSingle.Text}, but should be {selectedTargetResult}"); }
            else
            {
                foreach (IWebElement selectedTarget in _selectedTargetCheckboxes)
                {
                    Assert.IsTrue(selectedTarget.Text.ToLower().Contains(selectedTargetResult.ToLower()), $"Search result should contain {selectedTargetResult}, but is {selectedTarget.Text}");
                    break;
                }
            }
        }

        public void VerifySearchTargetResult(string selectedTargetResult)
        {
            GetWait().Until(driver => _searchResultField.Displayed);
            foreach (IWebElement selectedTarget in _selectedTargetCheckboxes)
            {
                Assert.IsTrue(selectedTarget.Text.ToLower().Contains(selectedTargetResult.ToLower()), $"Search result should contain {selectedTargetResult}, but is {selectedTarget.Text}");
            }
        }

        public void SetMaxPriceBySlider(double maxPrice)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", maxSlider);
            double sliderWidth = maxSlider.Size.Width;
            double sliderScaleWidth = _sliderScale.Size.Width;
            double maxPriceDefult = Int32.Parse(maxPriceDefault.GetAttribute("value"));
            int pixelsMoveToLeft = Convert.ToInt32(Math.Round((-(sliderScaleWidth - (maxPrice * (sliderScaleWidth) / maxPriceDefult)) + sliderWidth)));
            Actions action = new Actions(Driver);
            Actions moveSlider = new Actions(Driver);
            action.ClickAndHold(maxSlider).MoveByOffset(pixelsMoveToLeft, 0).Release().Build();
            action.Perform();
        }
        
        public void VerifyeHighestSetPrice(double maxPrice)
        {
            double[] allPrices = new double[itemPriceFields.Count];
            int i = 0;
            Thread.Sleep(5000);
            foreach (IWebElement price in itemPriceFields)
            {
                allPrices[i++] = Convert.ToDouble(price.Text.Split(' ')[0]);
            }
            Assert.IsTrue(allPrices.Max() < maxPrice, $"Max price {allPrices.Max()} of the item is more than {maxPrice}");
        }

        public void SelectBrandCheckboxAndVerifyItemsAmount(string brand)
        {
            string amountInSelectedBrandCheckbox = null;

            foreach (IWebElement checkbox in _brandsCheckboxes)
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                js.ExecuteScript("arguments[0].scrollIntoView();", checkbox);
                if (checkbox.Text.Contains(brand) && !(checkbox.Selected))
                {
                    amountInSelectedBrandCheckbox = ((checkbox.Text.Split(' '))[1].Trim().Trim('(', ')'));
                    checkbox.Click();
                    break;
                }
            }
            GetWait().Until(d => _brandsCheckboxes.ElementAt(0).Displayed);
            VerifyItemsAmount(amountInSelectedBrandCheckbox);
        }

        public void VerifyItemsAmount(string amountInSelectedBrandCheckbox)
        {
            Assert.AreEqual(amountInSelectedBrandCheckbox, _amountInSearchResultTitle.Text.Trim().Trim('(', ')'), "Serch results amount is not correct");
        }

        public void SelectItemByIndex(int itemIndex)
        {
            _items.ElementAt(itemIndex).Click();
        }
    }
}
