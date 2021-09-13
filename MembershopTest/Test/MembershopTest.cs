using MembershopTest.Page;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershopTest.Test
{
    public class MembershopTest : BaseTest
    {
        [TestCase("user", "password", false, "Neteisingas el. paštas arba slaptažodis!", TestName = "Test wrong user login")]
        [TestCase("gdz.testing@gmail.com", "KLAjoklis357", true, "Sveiki, Monika", TestName = "Test correct user login")]
        public static void TestLogin(string login, string password, bool loginSuccess, string resultLoginMessage)
        {
            _membershopHomePage.NavigateToPage();
            _membershopHomePage.AcceptCookies();
            _membershopHomePage.SelectLogIn();
            _membershopHomePage.InsertUserLoginData(login, password);
            _membershopHomePage.ClickLoginButton();
            _membershopHomePage.VerifyLoginResult(loginSuccess, resultLoginMessage);
        }


        [TestCase("PUDRA", 30, TestName = "Test search field for PUDRA and max price slide until 30")]
        public static void TestSearchAndSlide(string searchValue, double maxPrice)
        {
            _membershopHomePage.NavigateToPage();
            _membershopHomePage.AcceptCookies();
            _membershopHomePage.InsertSearchValue(searchValue);
            _membershopSearchResultPage.VerifySearchResult(searchValue);
            _membershopSearchResultPage.SetMaxPriceBySlider(maxPrice);
            _membershopSearchResultPage.VerifyeHighestSetPrice(maxPrice);
        }


        [TestCase("Papuošalai su natūraliais akmenimis", "Papuošalai", "Papuošalai su natūraliais akmenimis", true, "Moterims", TestName = "Test sale category Papuošalai su akmenimis and target group Moterims selection selection")]
        [TestCase("Babaria, Chi ir kt.: TOP kosmetikos prekiniai ženklai", "Moterims", "Babaria, Chi ir kt.: TOP kosmetikos prekiniai ženklai", false, "Moterims", TestName = "Test sale category Babaria and target group Moterims selection selection")]
        public static void TestSaleCategoryAndTargetGroupSelection(string saleCategoryTitle, string targetGroup, string saleCategoryTitleValidation, bool singleTargetResult, string targetGroupValidation)
        {
            _membershopHomePage.NavigateToPage();
            _membershopHomePage.AcceptCookies();
            _membershopHomePage.NavigateToAndOpenSaleCategory(saleCategoryTitle);
            _membershopSaleCategoryPage.SelectTargetGroup(targetGroup);
            _membershopSearchResultPage.VerifyTargetGroupAndSaleCategory(saleCategoryTitleValidation, singleTargetResult, targetGroupValidation);
        }


        [TestCase("lūpų dažai", "MAYBELLINE", TestName = "Test brand MAYBELLINE selection")]
        public static void TestBrandSelection(string searchValue, string brand)
        {
            _membershopHomePage.NavigateToPage();
            _membershopHomePage.AcceptCookies();
            _membershopHomePage.InsertSearchValue(searchValue);
            _membershopSearchResultPage.VerifySearchResult(searchValue);
            _membershopSearchResultPage.SelectBrandCheckboxAndVerifyItemsAmount(brand);
        }


        [TestCase("gdz.testing@gmail.com", "KLAjoklis357", 1, "Avalynė", "Moterims", 0, "37", "119.00", TestName = "Select item shoes size 37, add to cart, verify item price")]
        [TestCase("gdz.testing@gmail.com", "KLAjoklis357", 1, "Avalynė", "Moterims", 1, "36", "149.00", TestName = "Select item shoes size 36, add to cart, verify item price")]
        public static void TestAddItemIntoCart(string login, string password, int targetGroup, string itemsGroup, string selectedTargetResult, int selectItem, string size, string price)
        {
            _membershopHomePage.NavigateToPage();
            _membershopHomePage.AcceptCookies();
            _membershopHomePage.SelectLogIn();
            _membershopHomePage.InsertUserLoginData(login, password);
            _membershopHomePage.ClickLoginButton();
            _membershopHomePage.SelectTargetAntItemsGroup(targetGroup, itemsGroup);
            _membershopSearchResultPage.VerifySearchResult(itemsGroup);
            _membershopSearchResultPage.VerifySearchTargetResult(selectedTargetResult);
            _membershopSearchResultPage.SelectItemByIndex(selectItem);
            _membershopItemPage.SelectSize(size);
            _membershopItemPage.AddIntoCart();
            _membershopItemPage.OpenCartImmediately();
            _membershopCartPage.VerifySelectedSizeInCart(size);
            _membershopCartPage.VerifySelectedItemPriceInCart(price);
            _membershopHomePage.LogOut();
        }
    }
}
