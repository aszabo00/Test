using System.Security.Policy;
using OpenQA.Selenium;

namespace Test
{
    public class MyAccount
    {
        private IWebDriver driver { get; set; }

        public MyAccount(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Logs out.
        public MyAccount LogOut()
        {
            driver.CustomFindElement("xpath; //a[contains(text(), '(Logout)')]").Click();
            return this;
        }

        // Clicks the Your Details.
        public YourDetails ClickYourDetails()
        {
            driver.CustomFindElement("xpath; //div/a[contains(text(), 'Your Details')]").Click();

            return new YourDetails(driver);
        }

        // Signs existing customers in.
        public MyAccount SignIn(string username, string password)
        {
            driver.CustomFindElement("css; input[name='log']").SendKeys(username);
            driver.CustomFindElement("css; input[name='pwd']").SendKeys(password);
            driver.CustomFindElement("css; input[name='submit']").Click();
            return this;
        }
    }
}