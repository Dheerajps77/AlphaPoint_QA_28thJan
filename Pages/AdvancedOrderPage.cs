using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using log4net;
using OpenQA.Selenium;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Pages
{
    class AdvancedOrderPage
    {
        static ILog logger;
        private readonly ITestOutputHelper output;
        static Config data;
        public static IWebDriver driver;

        By buyTab = By.XPath("//div[text()='Buy']");
        By sellTab = By.XPath("//div[@class='advanced-order-sidepane__tab-container']/div[2]");
        By instrument = By.Name("instrument");
        By orderType = By.XPath("//select[@name='orderType']");
        By orderSize = By.XPath("//div[@class='ap-input__input-box advanced-order-form__input-box']//input[@name='quantity']");
        By limitPrice = By.XPath("//div[@class='ap-input__input-box advanced-order-form__input-box']//input[@name='limitPrice']");
        By displayQuntity = By.XPath("//div[@class='ap-input__input-box advanced-order-form__input-box']//input[@name='displayQuantity']");
        By placeByOrder = By.XPath("//form[@class='advanced-order-form__body']//button[text()='Place Buy Order']");
        By placeSellOrder = By.XPath("//form[@class='advanced-order-form__body']//button[text()='Place Sell Order']");
        By askOrBidPrice = By.XPath("//div[@class='advanced-order-form__limit-price-block-value']");
        By askOrBidPriceLabel = By.XPath("//div[@class='advanced-order-form__limit-price-block']/div");


        public AdvancedOrderPage(ITestOutputHelper output)
        {
            this.output = output;
            logger = APLogger.GetLog();
            logger.Info("Test Started");
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }


        public void SelectBuyOrSellTab(string buyOrSell)
        {
            Thread.Sleep(1000);
            try
            {
                if (buyOrSell.Equals("Buy"))
                {
                    string labeltext = driver.FindElement(askOrBidPriceLabel).Text;
                    if (!labeltext.Contains("Ask Price"))
                    {
                        driver.FindElement(buyTab).Click();
                    }
                }
                else if (buyOrSell.Equals("Sell"))
                {
                    driver.FindElement(sellTab).Click();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IWebElement InstrumentDropDown(IWebDriver driver)
        {
            return driver.FindElement(instrument);
        }

        public IWebElement OrderTypeDropDown(IWebDriver driver)
        {
            return driver.FindElement(orderType);
        }

        public IWebElement OrderSizeEditBox(IWebDriver driver)
        {
            return driver.FindElement(orderSize);
        }

        public IWebElement LimitPriceEditBox(IWebDriver driver)
        {
            return driver.FindElement(limitPrice);
        }

        public IWebElement DisplayQuntityEditBox(IWebDriver driver)
        {
            return driver.FindElement(displayQuntity);
        }

        public IWebElement PlaceByOrderButton(IWebDriver driver)
        {
            return driver.FindElement(placeByOrder);
        }

        public IWebElement PlaceSellOrderButton(IWebDriver driver)
        {
            return driver.FindElement(placeSellOrder);
        }

        public IWebElement AskOrBidPriceLabel(IWebDriver driver)
        {
            return driver.FindElement(askOrBidPrice);
        }


        public string GetAskOrBidPrice()
        {
            return driver.FindElement(askOrBidPrice).Text;
        }


        public void SelectInstrumentsAndOrderType(string instruments, string orderType)
        {
            try
            {
                UserSetFunctions.VerifyWebElement(InstrumentDropDown(driver));
                UserSetFunctions.SelectDropdown(InstrumentDropDown(driver), instruments);
                UserSetFunctions.VerifyWebElement(OrderTypeDropDown(driver));
                UserSetFunctions.SelectDropdown(OrderTypeDropDown(driver), orderType);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void PlaceBuyOrderWithReserveOrderType(string orderSize, string limitPrice, string displayQuantity)
        {
            try
            {
                UserSetFunctions.VerifyWebElement(OrderSizeEditBox(driver));
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.VerifyWebElement(LimitPriceEditBox(driver));
                UserSetFunctions.EnterText(LimitPriceEditBox(driver), limitPrice);
                UserSetFunctions.VerifyWebElement(DisplayQuntityEditBox(driver));
                UserSetFunctions.EnterText(DisplayQuntityEditBox(driver), displayQuantity);
                UserSetFunctions.VerifyWebElement(PlaceByOrderButton(driver));
                UserSetFunctions.Click(PlaceByOrderButton(driver));
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void PlaceSellOrderWithReserveOrderType(string orderSize, string limitPrice, string displayQuantity)
        {
            try
            {
                UserSetFunctions.VerifyWebElement(OrderSizeEditBox(driver));
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.VerifyWebElement(LimitPriceEditBox(driver));
                UserSetFunctions.EnterText(LimitPriceEditBox(driver), limitPrice);
                UserSetFunctions.VerifyWebElement(DisplayQuntityEditBox(driver));
                UserSetFunctions.EnterText(DisplayQuntityEditBox(driver), displayQuantity);
                UserSetFunctions.VerifyWebElement(PlaceSellOrderButton(driver));
                UserSetFunctions.Click(PlaceSellOrderButton(driver));
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void PlaceBuyOrderWithImmediateOrCancelType(string orderSize, string limitPrice)
        {
            try
            {
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.EnterText(LimitPriceEditBox(driver), limitPrice);
                UserSetFunctions.Click(PlaceByOrderButton(driver));
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void PlaceSellOrderWithImmediateOrCancelType(string orderSize, string limitPrice)
        {
            try
            {
                UserSetFunctions.EnterText(OrderSizeEditBox(driver), orderSize);
                UserSetFunctions.EnterText(LimitPriceEditBox(driver), limitPrice);
                UserSetFunctions.Click(PlaceSellOrderButton(driver));
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}