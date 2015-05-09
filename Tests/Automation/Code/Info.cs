using System.Collections.Generic;
using OpenQA.Selenium;

namespace Test
{
    public class Info
    {
        private IWebDriver driver { get; set; }

        public Info(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Clicks the purchase button.
        public Final ClickPurchase()
        {
            driver.CustomFindElement("css; div.input-button-buy").Click();
            
            return new Final(driver);
        }

        // Configures and calculates the shipping cost.
        public double CalculateShipping(string country, string stateProvince)
        {
            driver.CustomFindElement("xpath; //tr[@class='wpsc_change_country']//select[@id='current_country']//option[text()='{0}']; click".CustomFormat(country)).Click();

            driver.CustomFindElement("css; input[placeholder='State/Province']").SendKeys(stateProvince);
            driver.CustomFindElement("css; input[value='Calculate']").Click();

            return driver.CustomFindElement("css; span.pricedisplay.checkout-shipping").Text.ExtractDecimal();
        }

        // Signs existing customers in.
        public Info SignIn(string username, string password)
        {
            MyAccount myaccount = new MyAccount(driver);
            myaccount.SignIn(username, password);
            return this;
        }

        // Enters a customer email.
        public Info EnterEmail(string email)
        {
            driver.CustomFindElement("css; input[placeholder='Email']").SendKeys(email);
            return this;
        }

        // Enters a customers billing and contact details.
        public Info EnterBillingContactInfo(Dictionary<string, string> info)
        {
            YourDetails yourDetails = new YourDetails(driver);
            yourDetails.EnterBillingContactInfo(info);

            return this;
        }

        // Enters a customers shipping details. If the dictionary is null, the billing contact info is used.
        public Info EnterShippingAddress(Dictionary<string, string> info)
        {
            YourDetails yourDetails = new YourDetails(driver);
            yourDetails.EnterShippingAddress(info);

            return this;
        }
    }
}