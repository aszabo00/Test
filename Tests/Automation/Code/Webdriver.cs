using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace Test
{
    public static class Webdriver
    {
        // Creates the intended driver. Can add driver options here if neccessary.
        public static IWebDriver MakeDriver(string browser)
        {
            switch (browser)
            {
                case "firefox":            
                    return new FirefoxDriver();
                case "chrome":
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddExcludedArgument("ignore-certifcate-errors");
                    chromeOptions.AddArgument("test-type");
                    chromeOptions.AddArguments("--always-authorize-plugins=true");
                    DesiredCapabilities capabilities = DesiredCapabilities.Chrome();
                    capabilities.SetCapability(ChromeOptions.Capability, chromeOptions);
                    return new ChromeDriver(chromeOptions);
                default:
                    throw new System.ArgumentException("The browser {0} is not currently supported. FireFox and Chrome are currently supported.");
            }    
        }
    }
}
