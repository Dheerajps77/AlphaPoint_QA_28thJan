using AlphaPoint_QA.Common;
using AlphaPoint_QA.pages;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using log4net;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{

    //Test Secenario --> 2
    public class OrderEntryTest
    {
        private readonly ITestOutputHelper output;
        public static IWebDriver driver;
        static ILog logger;
        static Config data;
        string instrumentText = "BTCUSD";

        public OrderEntryTest(ITestOutputHelper output)
        {
            this.output = output;
            logger = APLogger.GetLog();
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        // Test Secenario - 3
        [Fact]
        public void VerifyPricePersistancy()
        {
            try
            {
                UserFunctions userfuntionality = new UserFunctions(output);
                userfuntionality.LogIn(logger);
                OrderEntryPage orderEntryPage = new OrderEntryPage(driver, output);
                Assert.True(orderEntryPage.VerifyOrderEntryAmountPersistence("20"));
                logger.Info("Market amount persistence test passed successfully");
            }
            catch(Exception e)
            {
                logger.Error("Market Amount Persistence Test Failed" );
                logger.Error(e.StackTrace);
                throw e;
            }
            finally
            {
                UserFunctions userFunctionality = new UserFunctions(output);
                userFunctionality.LogOut();
            }
        }

        //Test Secenario - 3
        //We are not seeing the order transaction message on completing a transaction. 
        [Fact]
        public void VerifyBuyMarketOrder()
        {
            try
            {
                UserFunctions userfuntionality = new UserFunctions(output);
                userfuntionality.LogIn(logger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, output);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab("BTCUSD", "Buy", 0.07));
                logger.Info("Verify buy market order type test passed successfully");
            }
            catch (Exception e)
            {
                logger.Error("Verify Buy Market Order Failed");
                logger.Error(e.StackTrace);
                throw e;
            }
            finally
            {
                UserFunctions userFunctionality = new UserFunctions(output);
                userFunctionality.LogOut();
            }
        }

        //Test Secenario - 4
        [Fact]
        public void VerifySellMarketOrder()
        {
            try
            {
                UserFunctions userfuntionality = new UserFunctions(output);
                userfuntionality.LogIn(logger);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, output);
                Assert.True(objVerifyOrdersTab.VerifyFilledOrdersTab("BTCUSD", "Sell", 0.07));
                logger.Info("Verified sell market Order test passed successfully");
            }
            catch (Exception e)
            {
                logger.Error("Verify Sell Market Order Failed");
                logger.Error(e.StackTrace);
                throw e;
            }
            finally
            {
                UserFunctions userFunctionality = new UserFunctions(output);
                userFunctionality.LogOut();
            }
        }

        //Test Secenario - 7
        [Fact]
        public void VerifyBuyStopOrder()
        {
            try
            {
                UserFunctions userFunctionality = new UserFunctions(output);
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, output);
                //objVerifyOrdersTab.VerifyOpenOrdersTab(instrumentText, "Buy", 1, 0.9);
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrumentText, "Buy", 1, 0.9));
                logger.Info("Verified buy stop Order test passed successfully");

            }
            catch (Exception e)
            {
                logger.Error("Verify Buy Stop Order Failed");
                logger.Error(e.StackTrace);
                throw e;
            }
            finally
            {
                UserFunctions userFunctionality = new UserFunctions(output);
                userFunctionality.LogOut();
            }

        }

        //Test Secenario - 8
        [Fact]
        public void VerifySellStopOrder()
        {
            try
            {
                VerifyOrdersTab objVerifyOrdersTab = new VerifyOrdersTab(driver, output);
                Assert.True(objVerifyOrdersTab.VerifyOpenOrdersTab(instrumentText, "Sell", 5, 35));
                logger.Info("Verified sell stop Order test passed successfully");
            }
            catch (Exception e)
            {
                logger.Error("Verify Sell Stop Order Failed");
                logger.Error(e.StackTrace);
                throw e;
            }
            finally
            {
                UserFunctions userFunctionality = new UserFunctions(output);
                userFunctionality.LogOut();
            }
        }

    }
}
