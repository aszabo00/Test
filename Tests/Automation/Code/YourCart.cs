using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Test
{
    public class YourCart
    {
        private IWebDriver driver { get; set; }

        public YourCart(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Goes to the info portion of the checkout.
        public Info GoToInfo()
        {
            driver.CustomFindElement("xpath; //a/span[contains(text(), 'Continue')]").Click();

            return new Info(driver);
        }

        // Finds the checkout screen pricing information for a product.
        public Dictionary<string, double> GetCartPricingInformation(string product)
        {
            Dictionary<string, double> productPricing = new Dictionary<string, double>();

            productPricing["sub-total"] = Convert.ToDouble(driver.CustomFindElement("xpath; //span[@class='yourtotal']//span[contains(text(), '$')]").Text);

            ICollection<IWebElement> productPriceElements = driver.CustomFindElements("xpath; //a[contains(text(), 'Apple iPhone 4S 16GB SIM-Free - Black')]/ancestor::tr//span[contains(text(), '$')]".CustomFormat(product));
            productPricing["price"] = productPriceElements.ElementAt(0).Text.ExtractDecimal();
            productPricing["total"] = productPriceElements.ElementAt(1).Text.ExtractDecimal();
            return productPricing;
        }

        // Removes all the products contained in the product string.
        public YourCart RemoveItemsFromCart(string product, string updatedQuantity)
        {
            driver.CustomFindElement("xpath; //a[contains(text(), '{0}')]/ancestor::tr//form[@class='adjustform qty']/input[@name='quantity']".CustomFormat(product.Replace('–', '-'))).SendNewKeys(updatedQuantity);
            driver.CustomFindElement("xpath; //a[contains(text(), '{0}')]/ancestor::tr//form[@class='adjustform qty']/input[@name='submit']".CustomFormat(product.Replace('–', '-'))).Click();            

            return this;
        }


    }
}