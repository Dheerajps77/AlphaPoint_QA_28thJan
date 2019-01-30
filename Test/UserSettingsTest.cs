using System;
using System.Collections.Generic;
using System.Text;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using log4net;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    public class UserSettingsTest
    {
        static ILog logger;
        private readonly ITestOutputHelper output;
        static Config data;
        public static IWebDriver driver;

        
        public UserSettingsTest(ITestOutputHelper output)
        {
            this.output = output;
            logger = APLogger.GetLog();
            logger.Info("Test Started");


            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        [Fact]
        public void VerifyAffiliateProgram()
        {
            // Admin login
            // Verify Trader has affiliate tag set up inadmin UI
            driver.Navigate().GoToUrl("https://apexwebqa.azurewebsites.net/exchange");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            //Login as a User say XYZ
            UserFunctions objUserFunctionality = new UserFunctions(output);
            objUserFunctionality.LogIn();

            // Note the number of affiliate programs
            UserSettingPage usp = new UserSettingPage(driver, output);
            Assert.True(usp.VerifyAffiliateProgramFunctionality(driver));

        }

        [Fact]
        public void VerifyCreateAPIKey()
        {
            driver.Navigate().GoToUrl("https://apexwebqa.azurewebsites.net/exchange");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            //Login as a User say XYZ
            UserFunctions objUserFunctionality = new UserFunctions(output);
            objUserFunctionality.LogIn();

            UserSettingPage usp = new UserSettingPage(driver, output);
            Assert.True(usp.CreateAPIkey(driver).Count>0);
        }

        [Fact]
        public void VerifyDeleteAPIKey()
        {
            driver.Navigate().GoToUrl("https://apexwebqa.azurewebsites.net/exchange");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            //Login as a User say XYZ
            UserFunctions objUserFunctionality = new UserFunctions(output);
            objUserFunctionality.LogIn();

            UserSettingPage usp = new UserSettingPage(driver, output);
            Assert.True(usp.DeleteAPIKey(driver));


        }
    }
}
