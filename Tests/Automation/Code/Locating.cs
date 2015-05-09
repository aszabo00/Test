using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Test
{
    public static class Locating
    {
        // Finds and returns an element from a string parameter containing "method; path".
        // "xpath; //button[@class='Class']"
        public static IWebElement CustomFindElement(this IWebDriver driver, string finderString)
        {
            driver.Sleep(2);

            // Parses the finderString and removes any spaces.
            string[] finderParams = finderString.Split(';');
            for (int i = 0; i < finderParams.Length; i++) finderParams[i] = finderParams[i].Trim();

            // Waits for the element then returns it once found.
            By by = FinderMethod(finderParams[0], finderParams[1]);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            return wait.Until(drv => drv.FindElement(by));
        }

        // Finds and returns the elements from a string parameter containing "method; path".
        // "xpath; //button[@class='Class']"
        public static ICollection<IWebElement> CustomFindElements(this IWebDriver driver, string finderString)
        {
            // Parses the finderString and removes any spaces.
            string[] finderParams = finderString.Split(';');
            for (int i = 0; i < finderParams.Length; i++) finderParams[i] = finderParams[i].Trim();

            // Waits for the element then returns it once found.
            By by = FinderMethod(finderParams[0], finderParams[1]);
            //return driver.FindElements(by);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(drv => drv.FindElements(by));
        }

        // Finds the method type to find an element.
        public static By FinderMethod(string method, string path)
        {
            switch (method)
            {
                case "xpath":
                    return By.XPath(path);
                case "css":
                    return By.CssSelector(path);
                default:
                    throw new System.ArgumentException("The finder method {0} is not supported. Xpath and CSS are currently supported.");
            }
        }
    }
}