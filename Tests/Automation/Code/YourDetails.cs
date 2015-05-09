using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Test
{
    public class YourDetails
    {
        private IWebDriver driver
        {
            get;
            set;
        }

        public YourDetails(IWebDriver driver)
        {
            this.driver = driver;
        }
        
        // Logs out.
        public MyAccount LogOut()
        {
            MyAccount myAccount = new MyAccount(driver);
            myAccount.LogOut();
            return new MyAccount(driver);
        }

        // Clicks save profile.
        public YourDetails ClickSaveProfile()
        {
            driver.CustomFindElement("css; input[value='Save Profile']").Click();
            return this;
        }
        
        // Enters a customers billing and contact details.
        public YourDetails EnterBillingContactInfo(Dictionary<string, string> info)
        {
            SetBillingFields(info, "Your billing/contact details");
            return this;
        }

        // Enters a customers shipping details. If the dictionary is null, the billing contact info is used.
        public YourDetails EnterShippingAddress(Dictionary<string, string> info)
        {
            if (info == null)
            {
                if (driver.CustomFindElement("xpath; //h4[contains(text(), 'Shipping Address')]/ancestor::tbody//span[@id='shippingsameasbillingmessage']").GetAttribute("style") != "display: inline;") driver.CustomFindElement("xpath; //h4[contains(text(), 'Shipping Address')]/ancestor::tbody//input[@id='shippingSameBilling']").Click();
            }
            else
            {
                if (driver.CustomFindElement("xpath; //h4[contains(text(), 'Shipping Address')]/ancestor::tbody//span[@id='shippingsameasbillingmessage']").GetAttribute("style") != "display: none;") driver.CustomFindElement("xpath; //h4[contains(text(), 'Shipping Address')]/ancestor::tbody//input[@id='shippingSameBilling']").Click();
                SetBillingFields(info, "Shipping Address");
            }

            return this;
        }

        // Verifies the customer shipping information.
        public YourDetails VerifyShippingInfo(Dictionary<string, string> infoEntered)
        {
            if (driver.CustomFindElement("xpath; //h4[contains(text(), 'Shipping Address')]/ancestor::tbody//span[@id='shippingsameasbillingmessage']").GetAttribute("style") != "display: none;") driver.CustomFindElement("xpath; //h4[contains(text(), 'Shipping Address')]/ancestor::tbody//input[@id='shippingSameBilling']").Click();
    
            Dictionary<string, string> infoFound = GetBillingFields(infoEntered, "Shipping Address");

            foreach (KeyValuePair<string, string> pair in infoEntered)
            {
                if ("State, Phone".Contains(pair.Key)) continue;
                Utilities.CustomStringAssert(infoEntered[pair.Key], infoFound[pair.Key]);
            }

            return this;
        }
        
        // Verifies the customer billing information.
        public YourDetails VerifyBillingInfo(Dictionary<string, string> infoEntered)
        {
            Dictionary<string, string> infoFound = GetBillingFields(infoEntered, "Your billing/contact details");
            
            foreach (KeyValuePair<string, string> pair in infoEntered)
            {
                if (pair.Key == "State") continue;
                Utilities.CustomStringAssert(infoEntered[pair.Key], infoFound[pair.Key]);
            }

            return this;
        }
        
        // The process of returning the billing/contact/shipping details within a section. Pass any dictionary with the right keys and value type.
        private Dictionary<string, string> GetBillingFields(Dictionary<string, string> info, string tableName)
        {
            Dictionary<string, string> infoFound = new Dictionary<string, string>(info);
            
            foreach (KeyValuePair<string, string> pair in info)
            {
                switch (pair.Key)
                {
                    case "Country":
                    infoFound[pair.Key] = driver.CustomFindElement("xpath; //h4[contains(text(), '{0}')]/ancestor::tbody//select[contains(@title, '{1}')]/ancestor::div[@class='selector']/span".CustomFormat(tableName, pair.Key.ToLower())).Text.Trim();
                        continue;
                    case "State":
                    case "State/Province":
                        continue;
                        infoFound[pair.Key] = "";
                    case "Address":
                        infoFound[pair.Key] = driver.CustomFindElement("xpath; //h4[contains(text(), '{0}')]/ancestor::tbody//textarea[@placeholder='{1}']".CustomFormat(tableName, pair.Key)).Text;
                        continue;
                    case "Phone":
                        if (tableName == "Shipping Address") continue;
                        break;
                }
                infoFound[pair.Key] = driver.CustomFindElement("xpath; //h4[contains(text(), '{0}')]/ancestor::tbody//input[@placeholder='{1}']".CustomFormat(tableName, pair.Key)).GetAttribute("value").Trim();
            }

            return infoFound;
        }
        
        // The process of filling out the billing/contact/shipping details within a section.
        private void SetBillingFields(Dictionary<string, string> info, string tableName)
        {
            foreach (KeyValuePair<string, string> pair in info)
            {
                switch (pair.Key)
                {
                    case "Country":
                        driver.CustomFindElement("xpath; //h4[contains(text(), '{0}')]/ancestor::tbody//select[contains(@title, '{1}')]/option[text()='{2}']".CustomFormat(tableName, pair.Key.ToLower(), pair.Value)).Click();
                        continue;
                    case "State":
                    case "State/Province":
                        continue;
                        driver.CustomFindElement("xpath; //h4[contains(text(), '{0}')]/ancestor::tbody//select[contains(@title, 'region')]/option[text()='{1}']".CustomFormat(tableName, pair.Value)).Click();
                    case "Address":
                        driver.CustomFindElement("xpath; //h4[contains(text(), '{0}')]/ancestor::tbody//textarea[@placeholder='{1}']".CustomFormat(tableName, pair.Key)).SendNewKeys(pair.Value);
                        continue;
                    case "Phone":
                        if (tableName == "Shipping Address") continue;
                        break;
                }

                driver.CustomFindElement("xpath; //h4[contains(text(), '{0}')]/ancestor::tbody//input[@placeholder='{1}']".CustomFormat(tableName, pair.Key)).SendNewKeys(pair.Value);
            }
        }
    }
}