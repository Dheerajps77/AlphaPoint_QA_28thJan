﻿using AlphaPoint_QA.Common;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using log4net;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{

    //Test Secenario --> 11
    public class AdvanceBuyLimitOrderIOCTest
    {
        private readonly ITestOutputHelper output;
        public static IWebDriver driver;
        static ILog logger;
        static Config data;
        static string username;
        static string password;

        string instrumentText = "BTCUSD";
        string orderTypeText = "Immediate or Cancel";
        string buySidetext = "Buy";
        string sellSidetext = "Sell";
        double orderSizeValue = 1;
        string limitPriceValue = "2";
        string timeInForceSelectTextValue = "Immediate or Cancel";
        string sellAmount = "0.77";
        string limitAmount = "0.77";
        string buyMrketOrderTime = "";
        By advanceOrderButton = By.XPath("//div[@class='order-entry__item-button' and text()='« Advanced Orders']");

        public AdvanceBuyLimitOrderIOCTest(ITestOutputHelper output)
        {
            this.output = output;
            logger = APLogger.GetLog();
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();

        }

        [Fact]
        public void VerifyAdvanceLimitOrderIOCConditions()
        {
            try
            {
                driver.Navigate().GoToUrl("https://apexwebqa.azurewebsites.net/exchange");
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                UserFunctions objUserFunctionality = new UserFunctions(output);
                objUserFunctionality.LogIn();

                AdvanceBuyLimitOrderIOCPage objAdvanceBuyLimitOrderIOCPage = new AdvanceBuyLimitOrderIOCPage(driver, output);

                if (objAdvanceBuyLimitOrderIOCPage.AdvanceOrdersButton(driver))
                {
                    //(First Condition)Below is used to place buy advance order with Order type is "Immediate or Cancel".
                    objAdvanceBuyLimitOrderIOCPage.AdvanceBuyLimitOrderIOC("Immediate or Cancel", "BTCUSD", "2", "1");
                    //Below is used to place sell advance order with Order type is "Immediate or Cancel".
                    objAdvanceBuyLimitOrderIOCPage.AdvanceSellLimitOrderIOC("Immediate or Cancel", "BTCUSD", "3", "1");

                    Thread.Sleep(2000);
                    UserSetFunctions.Click(driver.FindElement(advanceOrderButton));

                    Thread.Sleep(2000);
                    //(Second Condition)Below is used to place buy advance order with Order type is "Immediate or Cancel".
                    objAdvanceBuyLimitOrderIOCPage.AdvanceBuyLimitOrderIOC("Immediate or Cancel", "BTCUSD", "2", "1");
                    //Below is used to place sell advance order with Order type is "Immediate or Cancel".
                    objAdvanceBuyLimitOrderIOCPage.AdvanceSellLimitOrderIOC("Immediate or Cancel", "BTCUSD", "1", "1");

                    Thread.Sleep(2000);
                    UserSetFunctions.Click(driver.FindElement(advanceOrderButton));

                    Thread.Sleep(2000);
                    //(Third Condition)Below is used to place buy advance order with Order type is "Immediate or Cancel".
                    objAdvanceBuyLimitOrderIOCPage.AdvanceBuyLimitOrderIOC("Immediate or Cancel", "BTCUSD", "2", "1");
                    //Below is used to place sell advance order with Order type is "Immediate or Cancel".
                    objAdvanceBuyLimitOrderIOCPage.AdvanceSellLimitOrderIOC("Immediate or Cancel", "BTCUSD", "2", "1");
                }
            }
            catch (Exception e)
            {
                logger.Error("Verify Advance Limit Order IOC Conditions Unsuccessfull");
                logger.Error(e.StackTrace);
                throw e;
            }
        }
    }
}
