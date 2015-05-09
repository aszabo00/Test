using System;
using OpenQA.Selenium;

namespace Test
{
    public class ProductCategory
    {
        private IWebDriver driver { get; set; }

        public ProductCategory(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Decides if the customer will checkout or continue shopping.
        public YourCart GoToYourCart(bool checkout=true)
        {
            string decision = "go_to_checkout";
            if (!checkout) decision = "continue_shopping";
            driver.CustomFindElement("css; a.{0}".CustomFormat(decision)).Click();

            return new YourCart(driver);
        }
        
        // Clicks the add to cart button.
        public ProductCategory AddProductToCart(string product)
        {
            driver.CustomFindElement("xpath; //a[contains(text(), '{0}')]/ancestor::div[@class='productcol']//form//div[@class='input-button-buy']/span/input".CustomFormat(product)).Click();
            return this;
        }

        // Finds the pricing information for a product.
        public string[] GetProductPrice(string product)
        {
            IWebElement productPricesElement = driver.CustomFindElement("xpath; //h2[contains(text(), '{0}')]/ancestor::div[@class='productcol']//form//div[@class='wpsc_product_price']".CustomFormat(product));
            return productPricesElement.Text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }
        
        // Finds and clicks the category within the Product Category list.
        public ProductCategory SelectFromProductCategoryList(string category)
        {
            driver.Hover("xpath; //a[contains(text(), 'Product Category')]");
            driver.CustomFindElement("xpath; //a[contains(text(), 'Product Category')]/ancestor::li/ul[@class='sub-menu']/li/a[contains(text(), '{0}')]".CustomFormat(category)).Click();

            return this;
        }
    }
}