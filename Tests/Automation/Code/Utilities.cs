using System;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Test
{
    public static class Utilities
    {
        //Checks that two strings are the same.
        public static void CustomStringAssert(string expected, string actual)
        {
            if (expected != actual) throw new Exception("Expected: {0} | Actual: {0}".CustomFormat(expected, actual));
        }

        // Clears the field before entering new text.
        public static void SendNewKeys(this IWebElement element, string keys)
        {
            element.Clear();
            element.SendKeys(keys);
        }

        // Extracts a decimal number from a string.
        public static double ExtractDecimal(this string value)
        {
            return Convert.ToDouble(Regex.Replace(value, @"[^-?\d+\.]", ""));
        }
        
        // Hovers over an element.
        public static void Hover(this IWebDriver driver, string hoverFinderString, int x=-1, int y=-1)
        {
            Actions actions = new Actions(driver);    
            actions.MoveToElement(driver.CustomFindElement(hoverFinderString)).MoveByOffset( x, y ).Perform();
        }

        // "{0}".CustomFormat("A") functionality.
        // "{0}".CustomFormat("Hello") == "Hello".
        public static string CustomFormat(this string value, params object[] args)
        {
            return String.Format(value, args);
        }

        // Clicks an element then returns it.
        public static IWebElement ClickReturnElement(this IWebElement element)
        {
            element.Click();          
            return element;
        }

        // Sleeps.
        public static void Sleep(this IWebDriver driver, int seconds)
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }
    }
}