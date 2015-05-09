using OpenQA.Selenium;

namespace Test
{
    public class Tools
    {
        private IWebDriver driver { get; set; }

        public Tools(IWebDriver driver)
        {
            this.driver = driver;
        }

        public MyAccount ClickMyAccount()
        {
            driver.CustomFindElement("css; a.account_icon").Click();

            return new MyAccount(driver);
        }

        // Creates the object for 
        public ProductCategory Product()
        {
            return new ProductCategory(driver);
        }
    }
}