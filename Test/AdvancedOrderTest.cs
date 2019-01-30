using AlphaPoint_QA.Common;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using log4net;
using log4net.Config;
using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Test
{
    public class AdvancedOrderTest
    {

        static ILog logger;
        private readonly ITestOutputHelper output;
        static Config data;
        public static IWebDriver driver;
        string selectInstrument = "BTCUSD";
        string enterOrderSize = "1.1";


        public AdvancedOrderTest(ITestOutputHelper output)
        {
            this.output = output;
            logger = APLogger.GetLog();
            logger.Info("Test Started");
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }


        
        //Test Case-9
        //This test case is getting passed as there is no such expected message is defined to verify.
        //We will Verify the actual and expected message, once we get the expected message.
        [Fact]
        public void VerifyMarketOrderTypeAdvanceBuyOrder()
        {
            try
            {
                UserFunctions userfuntionality = new UserFunctions(output);
                userfuntionality.LogIn(logger);
                Thread.Sleep(2000);
                AdvanceBuyOrderPage objAdvanceBuyOrderPage = new AdvanceBuyOrderPage(driver, output);
                objAdvanceBuyOrderPage.VerifyAdvanceBuyOrder(selectInstrument, driver, enterOrderSize);
                Thread.Sleep(2000);

                string successMsg = UserCommonFunctions.GetTextOfSuccessfulMessage(driver, logger);
                logger.Info("Verfiy Market Order type Advance Buy Order passed successfully.");
            }
            catch (Exception e)
            {
                // add snapshot, logger then throw error
                logger.Error("Advance Buy Order Test Failed" + e);
                throw e;
            }
            finally
            {
                UserFunctions userFunctionality = new UserFunctions(output);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, logger);
                userFunctionality.LogOut();
            }
        }


        // Test Case- 10
        //This test case is getting passed as there is no such expected message is defined to verify.
        //We will Verify the actual and expected message, once we get the expected message.
        [Fact] 
        public void VerifyMarketOrderTypeAdvanceSellOrder()
        {
            try
            {
                UserFunctions userfuntionality = new UserFunctions(output);
                userfuntionality.LogIn(logger);
                Thread.Sleep(2000);
                AdvanceSellOrderPage objAdvanceSellOrderPage = new AdvanceSellOrderPage(driver, output);
                objAdvanceSellOrderPage.VerifyAdvanceSellOrder(selectInstrument, driver, enterOrderSize);

                Thread.Sleep(2000);

                string successMsg = UserCommonFunctions.GetTextOfSuccessfulMessage(driver, logger);
                logger.Info("Verify Market Order type Advance Sell Order passed successfully.");
            }
            catch (Exception e)
            {
                // add snapshot, logger then throw error
                logger.Error("Advance Sell Order Test Failed" + e);
                throw e;
            }
            finally
            {
                UserFunctions userFunctionality = new UserFunctions(output);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, logger);
                userFunctionality.LogOut();
            }
        }

        [Fact]   //25
        public void VerifyIOCAdvanceBuyOrder()
        {
            try
            {
                UserFunctions userfuntionality = new UserFunctions(output);
                userfuntionality.LogIn(logger);
                Thread.Sleep(3000);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.AdvanceOrder(driver);
                AdvancedOrderPage advanceorder = new AdvancedOrderPage(output);
                advanceorder.SelectBuyOrSellTab("Buy");
                advanceorder.SelectInstrumentsAndOrderType("BTCUSD", "Immediate or Cancel");
                advanceorder.PlaceBuyOrderWithImmediateOrCancelType("0.5", "532");

                string successMsg = UserCommonFunctions.GetTextOfSuccessfulMessage(driver, logger);
                Assert.Equal("Your order has been successfully added", successMsg);
                logger.Info("Verfiy Place By Order with Immediate Or Cancel Order Type passed successfully");
            }
            catch (Exception e)
            {
                GenericUtils.GetScreenshot(driver, "VerifyIOCAdvanceBuyOrder");
                logger.Error("Verify IOC Advance Buy Order Test Failed" + e);
                throw e;
            }
            finally
            {
                UserFunctions userFunctionality = new UserFunctions(output);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, logger);
                userFunctionality.LogOut();
            }

        }

        [Fact]      //26
        public void VerifyIOCAdvanceBuyOrderLimitAskPrice()
        {
            try
            {
                UserFunctions userFuntionality = new UserFunctions(output);
                userFuntionality.LogIn();
                Thread.Sleep(3000);
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectInstrumentFromExchange("BTCUSD", driver);
                string askPrice = UserCommonFunctions.GetAskPriceFromOrderBook(driver);
                UserCommonFunctions.AdvanceOrder(driver);

                AdvancedOrderPage advanceorder = new AdvancedOrderPage(output);
                advanceorder.SelectBuyOrSellTab("Buy");
                advanceorder.SelectInstrumentsAndOrderType("BTCUSD", "Immediate or Cancel");
                string reducedAskPrice = GenericUtils.ReducedAmount(askPrice);
                advanceorder.PlaceBuyOrderWithImmediateOrCancelType("0.5", reducedAskPrice);

                string successMsg = UserCommonFunctions.GetTextOfSuccessfulMessage(driver, logger);
                Assert.Equal("Your order has been successfully added", successMsg);
                logger.Info("Verify IOC Advance Buy Order Test limit ask price passed successfully.");
            }
            catch (Exception e)
            {
                GenericUtils.GetScreenshot(driver, "VerifyIOCAdvanceBuyOrderLimitAskPrice");
                logger.Error("Verify IOC Advance Buy Order Test Failed" + e); 
                throw e;
            }
            finally
            {
                UserFunctions userFunctionality = new UserFunctions(output);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, logger);
                userFunctionality.LogOut();

            }

        }

        [Fact]      //27
        public void VerifyPartiallyIOCAdvanceBuyOrderLimitAskPrice()
        {
            try
            {
                UserFunctions userFuntionality = new UserFunctions(output);
                userFuntionality.LogIn();
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectInstrumentFromExchange("BTCUSD", driver);
                string quantity = UserCommonFunctions.GetQuantityFromOrderBook(driver);
                UserCommonFunctions.AdvanceOrder(driver);

                AdvancedOrderPage advanceorder = new AdvancedOrderPage(output);
                advanceorder.SelectBuyOrSellTab("Buy");
                advanceorder.SelectInstrumentsAndOrderType("BTCUSD", "Immediate or Cancel");
                string askPrice = advanceorder.GetAskOrBidPrice();
                string increasedQuantity = GenericUtils.IncreseAmount(quantity);
                string reducedAskPrice = GenericUtils.ReducedAmount(askPrice);
                advanceorder.PlaceBuyOrderWithImmediateOrCancelType(increasedQuantity, reducedAskPrice);

                string successMsg = UserCommonFunctions.GetTextOfSuccessfulMessage(driver, logger);
                Assert.Equal("Your order has been successfully added", successMsg);
                logger.Info("Verify Partially IOC Advance Buy Order Limit Ask Price Test passed successfully.");
            }
            catch (Exception e)
            {
                GenericUtils.GetScreenshot(driver, "VerifyPartiallyIOCAdvanceBuyOrderLimitAskPrice");
                logger.Error("Verify Partially IOC Advance Buy Order Limit Ask Price Test Failed" + e);
                throw e;
            }
            finally
            {
                UserFunctions userFunctionality = new UserFunctions(output);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, logger);
                userFunctionality.LogOut();

            }

        }

        [Fact]   //28
        public void VerifyIOCAdvanceSellOrder()
        {
            try
            {
                UserFunctions userFuntionality = new UserFunctions(output);
                userFuntionality.LogIn();
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.AdvanceOrder(driver);

                AdvancedOrderPage advanceorder = new AdvancedOrderPage(output);
                advanceorder.SelectBuyOrSellTab("Sell");
                advanceorder.SelectInstrumentsAndOrderType("BTCUSD", "Immediate or Cancel");
                advanceorder.PlaceSellOrderWithImmediateOrCancelType("0.5", "532");

                string successMsg = UserCommonFunctions.GetTextOfSuccessfulMessage(driver, logger);
                Assert.Equal("Your order has been successfully added", successMsg);
                logger.Info("Verify IOC Advance Sell Order Test passed successfully.");
            }
            catch (Exception e)
            {
                GenericUtils.GetScreenshot(driver, "VerifyIOCAdvanceSellOrder");
                logger.Error("Verify IOC Advance Sell Order Test Failed" + e);
                throw e;
            }
            finally
            {
                UserFunctions userFuntionality = new UserFunctions(output);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, logger);
                userFuntionality.LogOut();

            }

        }

        [Fact]   //29
        public void VerifyIOCAdvanceSellOrderMoreThanBidPrice()
        {
            try
            {
                UserFunctions userFuntionality = new UserFunctions(output);
                userFuntionality.LogIn();
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.AdvanceOrder(driver);

                AdvancedOrderPage advanceorder = new AdvancedOrderPage(output);
                advanceorder.SelectBuyOrSellTab("Sell");
                advanceorder.SelectInstrumentsAndOrderType("BTCUSD", "Immediate or Cancel");
                string bidPrice = advanceorder.GetAskOrBidPrice();
                string increaseBidPrice = GenericUtils.IncreseAmount(bidPrice); ;
                advanceorder.PlaceSellOrderWithImmediateOrCancelType("0.5", increaseBidPrice);

                string successMsg = UserCommonFunctions.GetTextOfSuccessfulMessage(driver, logger);
                Assert.Equal("Your order has been successfully added", successMsg);
                logger.Info("Verify IOC Advance Sell Order More Than BidPrice Test passed successfully.");
            }
            catch (Exception e)
            {
                GenericUtils.GetScreenshot(driver, "VerifyIOCAdvanceSellOrderMoreThanBidPrice");
                logger.Error("Verify IOC Advance Sell Order More Than BidPrice Test Failed" + e);
                throw e;
            }
            finally
            {
                UserFunctions userFuntionality = new UserFunctions(output);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, logger);
                userFuntionality.LogOut();

            }

        }

        [Fact]   //30
        public void VerifyPartiallyIOCAdvanceSellOrderMoreThanBidPrice()
        {
            try
            {
                UserFunctions userFunctionality = new UserFunctions(output);
                userFunctionality.LogIn();
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectInstrumentFromExchange("BTCUSD", driver);
                string quantity = UserCommonFunctions.GetQuantityFromOrderBook(driver);
                Thread.Sleep(5000);
                UserCommonFunctions.AdvanceOrder(driver);

                AdvancedOrderPage advanceorder = new AdvancedOrderPage(output);
                advanceorder.SelectBuyOrSellTab("Sell");
                advanceorder.SelectInstrumentsAndOrderType("BTCUSD", "Immediate or Cancel");
                string bidPrice = advanceorder.GetAskOrBidPrice();
                string increasedQuantity = GenericUtils.IncreseAmount(quantity);
                string increaseBidPrice = GenericUtils.IncreseAmount(bidPrice); ;
                advanceorder.PlaceSellOrderWithImmediateOrCancelType(increasedQuantity, increaseBidPrice);

                string successMsg = UserCommonFunctions.GetTextOfSuccessfulMessage(driver, logger);
                Assert.Equal("Your order has been successfully added", successMsg);
                logger.Info("Verify Partially IOC Advance Sell Order More Than BidPrice Test passed successfully.");
            }
            catch (Exception e)
            {
                GenericUtils.GetScreenshot(driver, "VerifyPartiallyIOCAdvanceSellOrderMoreThanBidPrice");
                logger.Error("Verify Partially IOC Advance Sell Order More Than BidPrice Test Failed" + e); 
                throw e;
            }
            finally
            {
                UserFunctions userFunctionality = new UserFunctions(output);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, logger);
                userFunctionality.LogOut();

            }

        }

        [Fact]    //31
        public void VerifyPlaceBuyOrderWithReservOrderType()
        {
            try
            {
                UserFunctions userFunctionality = new UserFunctions(output);
                userFunctionality.LogIn();
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.AdvanceOrder(driver);

                AdvancedOrderPage advanceorder = new AdvancedOrderPage(output);
                advanceorder.SelectBuyOrSellTab("Buy");
                advanceorder.SelectInstrumentsAndOrderType("BTCUSD", "Reserve Order");
                advanceorder.PlaceBuyOrderWithReserveOrderType("2", "532", "1");

                string successmsg = UserCommonFunctions.GetTextOfSuccessfulMessage(driver, logger);
                Assert.Equal("Your order has been successfully added", successmsg);
                logger.Info("Verify Partially IOC Advance Sell Order More Than BidPrice Test passed successfully.");
            }
            catch (Exception e)
            {
                GenericUtils.GetScreenshot(driver, "VerifyPlaceBuyOrderWithReservOrderType");
                logger.Error("Verify Partially IOC Advance Sell Order More Than BidPrice Test Failed" + e); 
                throw e;
            }
            finally
            {
                UserFunctions userFunctionality = new UserFunctions(output);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, logger);
                userFunctionality.LogOut();
            }
        }

        [Fact]    //31
        public void VerifyPlaceSellOrderWithReserveOrderType()
        {
            try
            {
                UserFunctions userFunctionality = new UserFunctions(output);
                userFunctionality.LogIn();
                UserCommonFunctions.DashBoardMenuButton(driver);
                UserCommonFunctions.SelectAnExchange(driver);
                UserCommonFunctions.AdvanceOrder(driver);

                AdvancedOrderPage advanceorder = new AdvancedOrderPage(output);
                advanceorder.SelectBuyOrSellTab("Sell");
                advanceorder.SelectInstrumentsAndOrderType("BTCUSD", "Reserve Order");
                advanceorder.PlaceSellOrderWithReserveOrderType("2", "532", "1");

                string successmsg = UserCommonFunctions.GetTextOfSuccessfulMessage(driver, logger);
                Assert.Equal("Your order has been successfully added", successmsg);
                logger.Info("Verify Place Sell Order With Reserve Order Type Test passed successfully.");
            }
            catch (Exception e)
            {
                GenericUtils.GetScreenshot(driver, "VerifyPlaceSellOrderWithReserveOrderType");
                logger.Error("Verify Place Sell Order With Reserve Order Type Test Failed" + e);
                throw e;
            }
            finally
            {
                UserFunctions userFunctionality = new UserFunctions(output);
                UserCommonFunctions.CloseAdvancedOrderSection(driver, logger);
                userFunctionality.LogOut();
            }
        }


    }
}
