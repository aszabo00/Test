using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Test
{
    public class Final
    {
        private IWebDriver driver { get; set; }

        public Final(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Gets the pricing information of the Transaction Results for each product.
        public Dictionary<string, double> GetTransactionResults(string product)
        {
            Dictionary<string, double> transactionResults = new Dictionary<string, double>();

            ICollection<IWebElement> costElements = driver.CustomFindElements("xpath; //td[contains(text(), '{0}')]/ancestor::tr/td".CustomFormat(product));
            string[] totalPrices = driver.CustomFindElement("xpath; //p[contains(text(), 'Total Shipping:')]").Text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            transactionResults["Price"] = costElements.ElementAt(1).Text.ExtractDecimal();
            transactionResults["Quantity"] = costElements.ElementAt(2).Text.ExtractDecimal();
            transactionResults["Item Total"] = costElements.ElementAt(3).Text.ExtractDecimal();
            transactionResults["Total Shipping"] =totalPrices[0].ExtractDecimal();
            transactionResults["Total"] = totalPrices[1].ExtractDecimal();

            return transactionResults;
        }
    }
}