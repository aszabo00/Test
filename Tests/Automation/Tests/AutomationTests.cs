using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenQA.Selenium;
using NUnit.Framework;

namespace Test.Tests
{
    public class AutomationTests
    {
        public IWebDriver driver;
        public Dictionary<string, string> billingContactInfo;
        
        [SetUp]
        public void Create_Driver()
        {
            Cursor.Position = new Point(0, 0); 
            
            driver = Webdriver.MakeDriver("chrome");
            driver.Navigate().GoToUrl("http://store.demoqa.com/");

            billingContactInfo = new Dictionary<string, string>();
            billingContactInfo["First Name"] = "Ashton";
            billingContactInfo["Last Name"] = "Szabo";
            billingContactInfo["Address"] = "0000 SomePlace Ln";
            billingContactInfo["City"] = "SomeCity";
            billingContactInfo["State/Province"] = "SomeState";
            billingContactInfo["Country"] = "USA";
            billingContactInfo["Postal Code"] = "SomeZip";
            billingContactInfo["Phone"] = "SomeNumber";
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }

        [Test]
        public void PlaceOrderTest()
        {
            const string product = "Apple iPhone 4S 16GB SIM-Free – Black";

            Tools tools = new Tools(driver);

            ProductCategory productCategory = tools.Product();
            productCategory.SelectFromProductCategoryList("iPhones");
            productCategory.AddProductToCart(product);

            YourCart yourCart = productCategory.GoToYourCart(true);
            Info info = yourCart.GoToInfo();
            info.CalculateShipping("USA", "Texas");
            info.EnterEmail("aszabo00@gmail.com");
            info.EnterBillingContactInfo(billingContactInfo);
            info.EnterShippingAddress(null);
            
            Final final = info.ClickPurchase();
            Dictionary<string, double> finalCosts = final.GetTransactionResults(product);

            // Verifying the total price.
            Assert.AreEqual(finalCosts["Price"] * finalCosts["Quantity"] + finalCosts["Total Shipping"], finalCosts["Total"]);
        }

        [Test]
        public void MyAccountTest()
        {
            Tools tools = new Tools(driver);

            MyAccount myAccount = tools.ClickMyAccount();
            myAccount.SignIn("aszabo", "xHbnpmopUAYL");
            
            YourDetails yourDetails = myAccount.ClickYourDetails();
            yourDetails.EnterBillingContactInfo(billingContactInfo);
            yourDetails.EnterShippingAddress(null);

            yourDetails.ClickSaveProfile();
            yourDetails.LogOut();

            myAccount.SignIn("aszabo", "xHbnpmopUAYL");
            myAccount.ClickYourDetails();

            yourDetails.VerifyBillingInfo(billingContactInfo);
            yourDetails.VerifyShippingInfo(billingContactInfo);
        }

        [Test]
        public void EmptyCartTest()
        {
            const string product = "Apple iPhone 4S 16GB SIM-Free – Black";

            Tools tools = new Tools(driver);

            ProductCategory productCategory = tools.Product();
            productCategory.SelectFromProductCategoryList("iPhones");
            productCategory.AddProductToCart(product);

            YourCart yourCart = productCategory.GoToYourCart(true);
            yourCart.RemoveItemsFromCart(product, "0");
            string emptyCartMessage = driver.CustomFindElement("css; div.entry-content").Text.Trim();
            Utilities.CustomStringAssert("Oops, there is nothing in your cart.", emptyCartMessage);
        }
    }
}