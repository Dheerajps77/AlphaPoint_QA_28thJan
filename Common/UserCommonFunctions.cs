using AlphaPoint_QA.Utils;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Common
{
    class UserCommonFunctions
    {
        static ILog logger;
        static Config data;
        public static IWebDriver driver;
        static string username;
        static string password;

        public UserCommonFunctions()
        {
            logger = APLogger.GetLog();
        }

        static IJavaScriptExecutor js;
        static By dashBoardMenu = By.XPath("//div[@class='page-header-nav__menu-toggle']");
        static By selectExchangeLink = By.XPath("//a[@href='/exchange']");
        static By buyAndSell = By.XPath("//a[@href='/buy-sell']");
        static By userSetting = By.XPath("//a[@href='/settings/profile']");
        static By wallets = By.XPath("//a[@href='/wallets']");
        
        static By selectExchange = By.XPath("//*[@id='root']/div[1]/div[1]/div[2]/a[2]");
        static By clinkOnInstrument = By.XPath("//*[@id='root']/div[1]/div[2]/div[1]/div[1]/button");
        static By selectInstrumentDASCUSD = By.XPath("//*[@id='root']/div[1]/div[2]/div[1]/div[1]/div/div/div/div[3]/div[1]");
        static By selectInstrumentBTCUSD = By.XPath("//*[@id='root']/div[1]/div[2]/div[1]/div[1]/div/div/div/div[3]/div[2]");
        static By selectInstrumentETHCUSD = By.XPath("//*[@id='root']/div[1]/div[2]/div[1]/div[1]/div/div/div/div[3]/div[3]");
        static By selectInstrumentLTCUSD = By.XPath("//*[@id='root']/div[1]/div[2]/div[1]/div[1]/div/div/div/div[3]/div[4]");
        static By selectInstrumentLTCBTC = By.XPath("//*[@id='root']/div[1]/div[2]/div[1]/div[1]/div/div/div/div[3]/div[5]");
        static By selectInstrumentBTCEUR = By.XPath("//*[@id='root']/div[1]/div[2]/div[1]/div[1]/div/div/div/div[3]/div[6]");
        static By selectInstrumentFUELBTC = By.XPath("//*[@id='root']/div[1]/div[2]/div[1]/div[1]/div/div/div/div[3]/div[7]");
        static By selectInstrumentETHBTC = By.XPath("//*[@id='root']/div[1]/div[2]/div[1]/div[1]/div/div/div/div[3]/div[8]");
        static By getInstrumentName = By.XPath("//*[@id='root']/div[1]/div[2]/div[1]/div[1]/button/span[1]");

        static By signOutButton = By.XPath("//a[contains(@class,'popover-menu__item user-summary')]");
        static By userLogoButton = By.XPath("//button[contains(@class,'user-summary__popover-menu-trigger')]");
        static By balanceAmountInWallet = By.XPath("//div[@class='wallet-card__amount']//span");
        static By dashBoardMenuListItems = By.XPath("//a[contains(@class,'page-header-nav__item page-header-nav__item--hoverable')]");
     
        static By openOrder = By.XPath("//div[@class='ap-tab__menu order-history__menu']//div[@data-test='Open Orders']");
        static By filledOrder = By.XPath("//div[@class='ap-tab__menu order-history__menu']//div[@data-test='Filled Orders']");
        static By inactiveOrder = By.XPath("//div[@class='ap-tab__menu order-history__menu']//div[@data-test='Inactive Orders']");
        static By tradeOrder = By.XPath("//div[@class='ap-tab__menu order-history__menu']//div[@data-test='Trade Orders']");
        static By depositOrder = By.XPath("//div[@class='ap-tab__menu order-history__menu']//div[@data-test='Deposit Orders']");
        static By withdrawOrder = By.XPath("//div[@class='ap-tab__menu order-history__menu']//div[@data-test='Withdraw Orders']");
        static By askpriceinorderbook = By.XPath("//div[@class='flex-table__column orderbook__table-price orderbook__table-price--sell']/span");
        static By quantityinorderbook = By.XPath("//div[@class='flex-table__column orderbook__table-qty orderbook__table-qty--sell']/span");
        static By advanceOrderButton = By.XPath("//div[text()='« Advanced Orders']");

        static By selectServer = By.XPath("//select[@name='tradingServer']");
        static By userLoginName = By.XPath("//input[@name='username']");
        static By userLoginPassword = By.XPath("//input[@name='password']");
        static By userLoginButton = By.XPath("//button[text()='Log In']");
        static By exchangeButton = By.XPath("//span[text()='Exchange']");
        static By orderEntryTab = By.XPath("//div[text()='Order Entry']");
        static By advancedOrderLink = By.XPath("//div[text()='« Advanced Orders']");
        static By successMsg = By.XPath("//div[contains(@class,'snackbar snackbar')]/div");
        static By closeIconAdvancedOrder = By.XPath("//div[@class='ap-sidepane__close-button advanced-order-sidepane__close-button']/span");
        static By loggedInUserName = By.XPath("//button[@class='user-summary__popover-menu-trigger page-header-user-summary__popover-menu-trigger']");
        static By userSignOutButton = By.XPath("//span[contains(@class,'popover-menu__item-label') and text()='Sign Out']");

        public static IWebElement ExchangeButton()
        {
            return driver.FindElement(exchangeButton);
        }

        public static IWebElement OrderEntryTab()
        {
            return driver.FindElement(orderEntryTab);
        }

        public static IWebElement AdvancedOrderLink()
        {
            return driver.FindElement(advancedOrderLink);
        }

        //This method will click on exchange button
        public static IWebElement ExchangeButton(IWebDriver driver)
        {
            return driver.FindElement(exchangeButton);
        }

        //This method will click on Order Entry button
        public static IWebElement OrderEntryTab(IWebDriver driver)
        {
            return driver.FindElement(orderEntryTab);
        }

        //This method will click on Open Order Tab
        public static void OpenOrderTab(IWebDriver driver)
        {
            driver.FindElement(openOrder).Click();
        }

        //This method will click on filled Order Tab
        public static void FilledOrderTab(IWebDriver driver)
        {
            driver.FindElement(filledOrder).Click();
        }

        //This method will click on inactive Order Tab
        public static void inactiveTab(IWebDriver driver)
        {
            driver.FindElement(inactiveOrder).Click();
        }

        //This method will click on trade Order Tab
        public static void tradeTab(IWebDriver driver)
        {
            driver.FindElement(tradeOrder).Click();
        }

        //This method will click on deposit Order Tab
        public static void DepositTab(IWebDriver driver)
        {
            driver.FindElement(depositOrder).Click();
        }

        //This method will click on withdraw Order Tab
        public static void WithdrawTab(IWebDriver driver)
        {
            driver.FindElement(withdrawOrder).Click();
        }


        //This method will scroll to down till pixel defined by user
        public static void ScrollingDownVertical(IWebDriver driver)
        {
            js=(IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0, 300)");
        }

        //This method scroll to up till pixel defined by user
        public static void ScrollingUpVertical(IWebDriver driver)
        {
            js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0, -600)");
        }

        //This method scroll to Horizontally right 
        public static void ScrollingRightHorizontally(IWebDriver driver)
        {
            js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(500, 0)");
        }

        //This method scroll to up till pixel defined by user
        public static void ScrollingLeftHorizontally(IWebDriver driver)
        {
            js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(-500, 0)");
        }

        //This method scroll to particular webElement
        public static void ScrollingToParticularElement(IWebDriver driver, IWebElement iwebElement)
        {
            js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", iwebElement);
        }

        //This method scroll till Particular Coordinates
        public static void ScrollingToParticularCoordinates(IWebDriver driver)
        {
            js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(200,300)");
        }

        //This method is use to click on DashBoard button.
        public static void DashBoardMenuButton(IWebDriver driver)
        {
            GenericUtils.WaitForElementVisibility(driver, dashBoardMenu,30).Click();
            //driver.FindElement(dashBoardMenu).Click();
        }

        //This method is use select an Exchange.
        public static void SelectAnExchange(IWebDriver driver)
        {
            GenericUtils.WaitForElementVisibility(driver, selectExchangeLink, 30).Click();
           // driver.FindElement(selectExchangeLink).Click();
        }

        //This method Navigates to Exchange selects the Instrument
        public static void SelectInstrumentFromExchange(string instrument, IWebDriver driver)
        {

            GenericUtils.WaitForElementVisibility(driver, selectExchangeLink, 30).Click();
            Thread.Sleep(1000);
            driver.FindElement(clinkOnInstrument).Click();
            Thread.Sleep(1000);
            if (instrument.Equals("DASCUSD"))
            {
                try
                {
                    driver.FindElement(selectInstrumentDASCUSD).Click();
                }
                catch (StaleElementReferenceException ex)
                {
                    driver.FindElement(selectInstrumentDASCUSD).Click();
                }
            }
            else if (instrument.Equals("BTCUSD"))
            {
                try
                {
                    driver.FindElement(selectInstrumentBTCUSD).Click();
                }
                catch (StaleElementReferenceException )
                {
                    driver.FindElement(selectInstrumentBTCUSD).Click();
                }
            }
            else if (instrument.Equals("ETHCUSD"))
            {
                try
                {
                    driver.FindElement(selectInstrumentETHCUSD).Click();
                }
                catch (StaleElementReferenceException)
                {
                    driver.FindElement(selectInstrumentETHCUSD).Click();
                }

            }
            else if (instrument.Equals("LTCUSD"))
            {
                try
                {
                    driver.FindElement(selectInstrumentLTCUSD).Click();
                }
                catch (StaleElementReferenceException)
                {
                    driver.FindElement(selectInstrumentLTCUSD).Click();
                }
            }
            else if (instrument.Equals("LTCBTC"))
            {
                try
                {
                    driver.FindElement(selectInstrumentLTCBTC).Click();
                }
                catch (StaleElementReferenceException)
                {
                    driver.FindElement(selectInstrumentLTCBTC).Click();
                }
            }
            else if (instrument.Equals("BTCEUR"))
            {
                try
                {
                    driver.FindElement(selectInstrumentBTCEUR).Click();
                }
                catch (StaleElementReferenceException)
                {
                    driver.FindElement(selectInstrumentBTCEUR).Click();
                }
            }
            else if (instrument.Equals("FUELBTC"))
            {
                try
                {
                    driver.FindElement(selectInstrumentFUELBTC).Click();
                }
                catch (StaleElementReferenceException)
                {
                    driver.FindElement(selectInstrumentFUELBTC).Click();
                }
            }
            else if (instrument.Equals("ETHBTC"))
            {
                try
                {
                    driver.FindElement(selectInstrumentETHBTC).Click();
                }
                catch (StaleElementReferenceException)
                {
                    driver.FindElement(selectInstrumentETHBTC).Click();
                }
            }
        }



        //This method will click on "Buy & Sell" from Dashboard menu
        public static void NavigateToBuySell(IWebDriver driver)
        {
            driver.FindElement(buyAndSell).Click();
        }

        //This method will click on "User Settings" from Dashboard menu
        public static void NavigateToUserSetting(IWebDriver driver)
        {
            driver.FindElement(userSetting).Click();
        }

        //This method will click on "Wallets" from Dashboard menu
        public static void NavigateToWallets(IWebDriver driver)
        {
            driver.FindElement(wallets).Click();
        }

        //This method will click on "LogOut" in the page
        public static void LogOut(IWebDriver driver)
        {
            driver.FindElement(userLogoButton).Click();
            driver.FindElement(signOutButton).Click();
        }

        // This method will close the Browser
        public static void CloseBrowser(IWebDriver driver)
        {
            driver.Close();
        }

        //This method will click on advance order button
        public static void AdvanceOrder(IWebDriver driver)
        {
            ScrollingDownVertical(driver);
            driver.FindElement(advanceOrderButton).Click();
        }

        //This method will get Ask Price from OrderBook
        public static string GetAskPriceFromOrderBook(IWebDriver driver)
        {
            return driver.FindElement(askpriceinorderbook).Text;
        }

        //This method will get Quantity from Order Book
        public static string GetQuantityFromOrderBook(IWebDriver driver)
        {
            return driver.FindElement(quantityinorderbook).Text;
        }

        //This method stores the Amount Balances for Order Entry Page
        public static Dictionary<string, string> StoreOrderEntryAmountBalances(IWebDriver driver)
        {
            string marketPrice = driver.FindElement(By.XPath("//label[contains(@class,'ap--label ap-label-with-text__label') and text()='Market Price']//following::span[@data-test='Market Price']")).Text;
            string orderTotal = driver.FindElement(By.XPath("//label[contains(@class,'ap--label ap-label-with-text__label') and text()='Order Total']//following::span[@class='ap-label-with-text__text order-entry__lwt-text']")).Text;
            Dictionary<string, string> amountDetailsList = new Dictionary<string, string>();
            amountDetailsList.Add("Market Price", marketPrice);
            amountDetailsList.Add("Order Total", orderTotal);
            return amountDetailsList;
        }

        public static void SelectExchange(ILog logger)
        {
            try
            {
                UserSetFunctions.Click(ExchangeButton());
                Thread.Sleep(2000);
                string currenturl = driver.Url;
                Assert.Contains("exchange", currenturl);
            }
            catch (Exception e)
            {
                logger.Error("Select Exchange failed");
                logger.Error(e.StackTrace);
                throw e;
            }
        }

        public static void AdvanceOrder(ILog logger)
        {
            try
            {
                UserSetFunctions.Click(OrderEntryTab());
                UserSetFunctions.Click(AdvancedOrderLink());
            }
            catch (Exception e)
            {
                logger.Error("Click operation failed on Advance Order link");
                logger.Error(e.StackTrace);
                throw e;
            }
        }

        public static string GetTextOfSuccessfulMessage(IWebDriver driver, ILog logger)
        {
            try
            {
                IWebElement successMsgWebElement = GenericUtils.WaitForElementVisibility(driver, successMsg, 10);
                string successText = successMsgWebElement.Text;
                return successText;
            }
            catch (Exception e)
            {
                logger.Error("Failed to get text of Success Message");
                logger.Error(e.StackTrace);
                throw e;
            }
        }

        public static void CloseAdvancedOrderSection(IWebDriver driver, ILog logger)
        {
            try
            {
                IWebElement closeAdvanced = GenericUtils.WaitForElementVisibility(driver, closeIconAdvancedOrder, 30);
                UserSetFunctions.Click(closeAdvanced);
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                logger.Error("Close Advanced Order Section Failed");
                logger.Error(e.StackTrace);
                throw e;
            }
        }
    }
}

