using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using log4net;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;


namespace AlphaPoint_QA.pages
{
    public class OrderEntryPage
    {
        IWebDriver driver;
        private readonly ITestOutputHelper output;
        static ILog logger;
        readonly string exchangeMenuString = "Exchange";

        public OrderEntryPage(IWebDriver driver, ITestOutputHelper output)
        {
            this.driver = driver;
            this.output = output;
            logger = APLogger.GetLog();
        }

        /// <summary>
        /// Locators for elements
        /// </summary>
        public By orderEntryButton = By.XPath("//div[@data-test='Order Entry']");
        public By buyOrderEntryButton = By.XPath("//label[@data-test='Buy Side']");
       
        // Order Type Market
        public By marketOrderTypeButton = By.XPath("//label[@data-test='Market Order Type']");
        public By buyAmountTextField = By.XPath("//input[@data-test='Buy Amount']");
        public By placeBuyOrderButton = By.XPath("//button[text()='Place Buy Order']");
        public By lastPrice = By.XPath("//span[@class='instrument-table__value instrument-table__value--last-price']");
        public By placeSellOrderButton = By.XPath("//button[text()='Place Sell Order']");
        public By stopOrderTypeButton = By.XPath("//label[@data-test='Stop Order Type']");    
        public By sellAmountTextField = By.XPath("//input[@data-test='Sell Amount']");       
        public By limitOrderTypeButton = By.XPath("//label[@data-test='Limit Order Type']");      
        public By sellOrderEntryButton = By.XPath("//label[@data-test='Sell Side']");
        
        public By feesText = By.XPath("//div[contains(@class,'ap-label-with-text')]//label[contains(@class,'order-entry__lwt-label') and text()='Fees']");
        public By orderTotalText = By.XPath("//label[contains(@class,'ap--label ap-label-with-text__label') and text()='Order Total']//following::span[@class='ap-label-with-text__text order-entry__lwt-text']");
        public By netText = By.XPath("//div[contains(@class,'ap-label-with-text')]//label[contains(@class,'order-entry__lwt-label') and text()='Net']");
        public By marketPriceText = By.XPath("//label[contains(@class,'ap--label ap-label-with-text__label') and text()='Market Price']//following::span[@data-test='Market Price']");
        public By stopPriceTextField = By.XPath("//input[@data-test='Stop Price']");
        public By exchangeMenuText = By.XPath("//span[@class='page-header-nav__label' and text()='Exchange']");
        public By successfullymsg = By.XPath("//div[contains(@class,'snackbar snackbar')]/div");

        public IWebElement PlaceBuyOrderButton()
        {
            return driver.FindElement(placeBuyOrderButton);
        }

        public IWebElement TransactionMessage()
        {
            return driver.FindElement(successfullymsg);
        }

        public IWebElement PlaceSellOrderButton()
        {
            return driver.FindElement(placeSellOrderButton);
        }

        public IWebElement StopOrderTypeButton()
        {
            return driver.FindElement(stopOrderTypeButton);
        }

        public IWebElement BuyAmountTextField()
        {
            return driver.FindElement(buyAmountTextField);
        }

        public IWebElement SellAmountTextField()
        {
            return driver.FindElement(sellAmountTextField);
        }

        public IWebElement MarketPriceText()
        {
            return driver.FindElement(marketPriceText);
        }
        
        public IWebElement MarketOrderTypeButton()
        {
            return driver.FindElement(marketOrderTypeButton);
        }
        public IWebElement StopPriceTextField()
        {
            return driver.FindElement(stopPriceTextField);
        }
        

        public IWebElement LimitOrderTypeButton()
        {
            return driver.FindElement(limitOrderTypeButton);
        }

        public IWebElement BuyOrderEntryButton()
        {
            return driver.FindElement(buyOrderEntryButton);
        }

        public IWebElement SellOrderEntryButton()
        {
            return driver.FindElement(sellOrderEntryButton);
        }

        public IWebElement OrderEntryButton()
        {
            return driver.FindElement(orderEntryButton);
        }

        public IWebElement FeesText()
        {
            return driver.FindElement(feesText);
        }

        public IWebElement OrderTotalText()
        {
            return driver.FindElement(orderTotalText);
        }

        public IWebElement NetText()
        {
            return driver.FindElement(netText);
        }

        public IWebElement ExchangeMenuText()
        {
            return driver.FindElement(exchangeMenuText);
        }

        // This method fetches the Last Price value
        public string GetLastPrice()
        {
            output.WriteLine("Last Price"+driver.FindElement(lastPrice).Text);
            return driver.FindElement(lastPrice).Text;
        }

        // This method verifies the persistence of Amount entered in the Order Size field
        public bool VerifyOrderEntryAmountPersistence(string amountEntered)
        {
            bool flag = false;
            string exchangeStringValueFromSite;
            UserCommonFunctions.DashBoardMenuButton(driver);
            UserCommonFunctions.SelectAnExchange(driver);
            Thread.Sleep(2000);
            exchangeStringValueFromSite = ExchangeMenuText().Text;
            Thread.Sleep(2000);

            if (exchangeStringValueFromSite.Equals(exchangeMenuString))
            {
                logger.Info("Verification for exchangeMenu value has been passed.");
                UserSetFunctions.Click(MarketOrderTypeButton());
                UserSetFunctions.EnterText(BuyAmountTextField(), amountEntered);
                UserSetFunctions.Click(LimitOrderTypeButton());
                UserSetFunctions.Click(StopOrderTypeButton());
                UserSetFunctions.Click(MarketOrderTypeButton());
                Thread.Sleep(2000);
                string amountPersisted = BuyAmountTextField().GetAttribute("value");
                if (amountEntered.Equals(amountPersisted))
                {
                    logger.Info("Test case has been passed for Buy Market Order Type.");
                    flag = true;
                }
                else
                {
                    logger.Info("Test case has been failed for Buy Market Order Type.");
                    flag = false;
                }

                UserSetFunctions.Click(SellOrderEntryButton());
                UserSetFunctions.EnterText(SellAmountTextField(), amountEntered);
                UserSetFunctions.Click(MarketOrderTypeButton());
                UserSetFunctions.Click(LimitOrderTypeButton());
                UserSetFunctions.Click(StopOrderTypeButton());

                Thread.Sleep(2000);
                if (amountEntered.Equals(amountPersisted))
                {
                    logger.Info("Test case has been passed for Sell Market Order Type.");
                    flag = true;
                }
                else
                {
                    logger.Info("Test case has been failed for Sell Market Order Type.");
                    flag = false;
                }
            }
            else
            {
                logger.Info("Verification for exchangeMenu value has been failed.");
                flag = false;
            }
            return flag;
        }

        
        public string PlaceMarketBuyOrder(double buyAmount)
        {
            //UserFunctions objUserFunctionality = new UserFunctions(output);
            UserSetFunctions.Click(OrderEntryButton());
            UserSetFunctions.Click(BuyOrderEntryButton());
            UserSetFunctions.Click(MarketOrderTypeButton());
            UserSetFunctions.EnterText(BuyAmountTextField(), buyAmount.ToString());
            Thread.Sleep(2000);
            UserSetFunctions.Click(PlaceBuyOrderButton());
            Thread.Sleep(3000);

            // We will use below comments code once we get the proper order transaction message in the site.
            //string verifytransactionsMesg = UserCommonFunctions.GetTextOfSuccessfulMessage(driver, logger);
            //logger.Info("Current order transaction message ---> " + verifytransactionsMesg);

           UserCommonFunctions.ScrollingDownVertical(driver);
            Thread.Sleep(3000);
            return GenericUtils.GetCurrentTime();

        }

        public string PlaceMarketSellOrder(double sellAmount)
        {
            //UserFunctions objUserFunctionality = new UserFunctions(output);
            UserSetFunctions.Click(driver.FindElement(orderEntryButton));
            UserSetFunctions.Click(driver.FindElement(sellOrderEntryButton));
            UserSetFunctions.Click(driver.FindElement(marketOrderTypeButton));
            UserSetFunctions.EnterText(SellAmountTextField(), sellAmount.ToString());
            Thread.Sleep(2000);
            UserSetFunctions.Click(PlaceSellOrderButton());
            Thread.Sleep(3000);

            string verifytransactionsMesg = UserCommonFunctions.GetTextOfSuccessfulMessage(driver, logger);
            logger.Info("Current order transaction message ---> "+ verifytransactionsMesg);

            UserCommonFunctions.ScrollingDownVertical(driver);
            Thread.Sleep(3000);

            return GenericUtils.GetCurrentTime();
        }

        public string PlaceStopBuyOrder(double buyAmount, double stopPrice)
        {
            //UserFunctions objUserFunctionality = new UserFunctions(output);
            if (BuyOrderEntryButton().Displayed && StopOrderTypeButton().Displayed)
            {
                logger.Info("For Buy Stop Order case ---> Balances stored successfully.");
                UserSetFunctions.Click(OrderEntryButton());
                UserSetFunctions.Click(BuyOrderEntryButton());
                UserSetFunctions.Click(StopOrderTypeButton());
                UserSetFunctions.EnterText(BuyAmountTextField(), buyAmount.ToString());
                UserSetFunctions.EnterText(StopPriceTextField(), stopPrice.ToString());
                Thread.Sleep(2000);

                // Verify Market Price, Fees and Order Total
                Dictionary<string, string> balances = new Dictionary<string, string>();

                if (OrderTotalText().Enabled && MarketPriceText().Enabled)
                {
                    // Storing balances in Dictionary
                    balances = UserCommonFunctions.StoreOrderEntryAmountBalances(driver);
                    logger.Info("For Buy Stop Order case ---> Balances stored successfully.");
                }
                else
                {
                    logger.Error("For Buy stop Order case ---> Market or Order Total or Net amount is not present");
                }
                UserSetFunctions.Click(PlaceBuyOrderButton());
                Thread.Sleep(2000);
                string verifyTransactionsMesg = UserCommonFunctions.GetTextOfSuccessfulMessage(driver, logger);
                logger.Info("Current order transaction message ---> " + verifyTransactionsMesg);
                UserCommonFunctions.ScrollingDownVertical(driver);
                Thread.Sleep(2000);
                return GenericUtils.GetCurrentTime();
            }
            else
            {
                logger.Error("For Buy Stop Order case ---> Balances stored successfully.");
                return GenericUtils.GetCurrentTime();
            }
        }

        public string PlaceStopSellOrder(double sellAmount, double stopPrice)
        {
            //UserFunctions objUserFunctionality = new UserFunctions(output);
            if (SellOrderEntryButton().Displayed&& StopOrderTypeButton().Displayed)
            {
                logger.Info("For Sell Stop Order case ---> Balances stored successfully.");         
            }
            else
            {
                logger.Error("For Sell stop Order case ---> Market or Order Total or Net amount is not present");  
            }
            UserSetFunctions.Click(OrderEntryButton());       
            UserSetFunctions.Click(SellOrderEntryButton());      
            UserSetFunctions.Click(StopOrderTypeButton());
            UserSetFunctions.EnterText(SellAmountTextField(), sellAmount.ToString());
            UserSetFunctions.EnterText(StopPriceTextField(), stopPrice.ToString());

            Thread.Sleep(3000);

            // Verify Market Price, Fees and Order Total
            Dictionary<string, string> balances = new Dictionary<string, string>();

            if (OrderTotalText().Enabled && MarketPriceText().Enabled)
            {

                // Storing balances in Dictionary
                balances = UserCommonFunctions.StoreOrderEntryAmountBalances(driver);
                logger.Info("For Sell Stop Order case ---> Balances stored successfully.");
            }
            else
            {
                logger.Error("For Sell stop Order case ---> Market or Order Total or Net amount is not present");
            }
            Thread.Sleep(2000);
            UserSetFunctions.Click(PlaceSellOrderButton());
            Thread.Sleep(3000);
            string verifytransactionsMesg = UserCommonFunctions.GetTextOfSuccessfulMessage(driver, logger);
            logger.Info("Current order transaction message ---> " + verifytransactionsMesg);
            UserCommonFunctions.ScrollingDownVertical(driver);
            Thread.Sleep(3000);
            return GenericUtils.GetCurrentTime();


        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           