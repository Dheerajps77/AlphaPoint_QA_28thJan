using AlphaPoint_QA.Common;
using AlphaPoint_QA.pages;
using AlphaPoint_QA.Utils;
using log4net;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Pages
{
    class VerifyOrdersTab
    {
        IWebDriver driver;
        private readonly ITestOutputHelper output;
        static ILog logger;

        public VerifyOrdersTab(IWebDriver driver, ITestOutputHelper output)
        {
            this.driver = driver;
            this.output = output;
            logger = APLogger.GetLog();

        }

        By orderRows = By.XPath("//div[@class='flex-table__body order-history-table__body']/div");

        public int CountOfOrderRows()
        {
            return driver.FindElements(orderRows).Count;
        }

        //This method will verify the order placed in Filled orders tab through Order Entry 
        public bool VerifyFilledOrdersTab(string instrument, string side, double size)
        {
            var flag = false;
            string marketOrderTime = null;

            UserCommonFunctions.DashBoardMenuButton(driver);
            Thread.Sleep(2000);
            UserCommonFunctions.SelectAnExchange(driver);

            OrderEntryPage boe = new OrderEntryPage(driver, output);
            
            if (side.Equals("Buy"))
            {
                marketOrderTime = boe.PlaceMarketBuyOrder(size);
            }

            else if(side.Equals("Sell"))
            {
                marketOrderTime = boe.PlaceMarketSellOrder(size);
            }
            
            string buyAmountValue = GenericUtils.ConvertToDoubleFormat(size);
            string lastPrice = boe.GetLastPrice();
            double doubleLastPrice = Convert.ToDouble(lastPrice);
            string totalAmountCalculated = GenericUtils.FilledOrdersTotalAmount(size, doubleLastPrice);
            Thread.Sleep(2000);
            UserCommonFunctions.FilledOrderTab(driver);

            string expectedRow = instrument + " || " + side + " || " + size + " || " + lastPrice + " || " + totalAmountCalculated + " || " + marketOrderTime;
            if (GetListOfFilledOrders().Contains(expectedRow))
            {
                logger.Info(side + "Order Successfully verifed in Filled orders tab");
                flag = true;
            }
            return flag;

        }

        //This method will verify the order placed in Open orders tab through Order Entry 
        public bool VerifyOpenOrdersTab(string instrument, string side, double sizeAmount, double stopPrice)
        {
            try { 
            var flag = false;
            string marketStopOrderTime = null;

            UserFunctions userfuntionality = new UserFunctions(output);
            userfuntionality.LogIn(logger);

            UserCommonFunctions.DashBoardMenuButton(driver);
            Thread.Sleep(2000);
            UserCommonFunctions.SelectAnExchange(driver);

            OrderEntryPage boe = new OrderEntryPage(driver, output);

            if (side.Equals("Buy"))
            {
                marketStopOrderTime = boe.PlaceStopBuyOrder(sizeAmount, stopPrice);
            }
            else if (side.Equals("Sell"))
            {
                marketStopOrderTime = boe.PlaceStopSellOrder(sizeAmount, stopPrice);
            }
            string buyAmountValue = GenericUtils.ConvertToDoubleFormat(sizeAmount);
            string lastPrice = boe.GetLastPrice();
            double doubleLastPrice = Convert.ToDouble(lastPrice);
            string totalAmountCalculated = GenericUtils.FilledOrdersTotalAmount(sizeAmount, doubleLastPrice);

            UserCommonFunctions.OpenOrderTab(driver);

            string expectedRow = instrument + " || " + side + " || " + sizeAmount + " || " + lastPrice + " || " + totalAmountCalculated + " || " + marketStopOrderTime;

            if (GetListOfFilledOrders().Contains(expectedRow))
            {
                logger.Info(side + "Order Successfully verifed in Open orders tab");
                flag = true;
            }
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method returns the list of all filled orders 
        public ArrayList GetListOfFilledOrders()
        {
            ArrayList filledOrderList = new ArrayList();
            int countOfFilledOrders = CountOfOrderRows();
            for (int i = 1; i <= countOfFilledOrders; i++)
            {
                String textFinal = "";
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div")).Count;
                for (int j = 2; j <= (countItems); j++)
                {
                    String text = driver.FindElement(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div[" + j + "]")).Text;
                    if (j == 2)
                    {
                        textFinal = text;
                    }
                    else
                    {
                        if (j == 8)
                        {
                            continue;
                        }
                        textFinal = textFinal + " || " + text;
                    }

                }
                filledOrderList.Add(textFinal);
            }
            return filledOrderList;
        }

        //This method returns the list of all open orders 
        public ArrayList GetListOfOpenOrders()
        {
            ArrayList openOrderList = new ArrayList();
            int countOfOpenOrders = CountOfOrderRows();
            for (int i = 1; i <= countOfOpenOrders; i++)
            {
                String textFinal = "";
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div")).Count;
                for (int j = 2; j <= (countItems - 2); j++)
                {
                    String text = driver.FindElement(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div[" + j + "]")).Text;
                    if (j == 2)
                    {
                        textFinal = text;
                    }
                    else
                    {
                        textFinal = textFinal + " || " + text;
                    }

                }
                openOrderList.Add(textFinal);
            }
            return openOrderList;
        }

        //This method returns the list of all Inactive orders 
        public ArrayList GetListOfInactiveOrders()
        {
            ArrayList inactiveOrderList = new ArrayList();
            int countOfInactiveOrders = CountOfOrderRows();
            for (int i = 1; i <= countOfInactiveOrders; i++)
            {
                String textFinal = "";
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div")).Count;
                for (int j = 2; j <= (countItems - 2); j++)
                {
                    String text = driver.FindElement(By.XPath("(//div[@class='flex-table__body order-history-table__body']/div)[" + i + "]/div[" + j + "]")).Text;
                    if (j == 2)
                    {
                        textFinal = text;
                    }
                    else
                    {
                        textFinal = textFinal + " || " + text;
                    }

                }
                inactiveOrderList.Add(textFinal);
            }
            return inactiveOrderList;
        }

        //This method is used to wait for disabled button.
        public ArrayList WaitForButtonDisable(String buttonTitle)
        {

            ArrayList dateTimeList = new ArrayList();

            String dateTime = "";
            String dateTimeMinusOne = "";
            for (int i = 0; i <= 100; i++)
            {

                String cssCursorValue = driver.FindElement(By.XPath("//button[text()='" + buttonTitle + "']")).GetCssValue("cursor");
                if (cssCursorValue.Equals("not-allowed"))
                {
                    dateTimeList.Add(dateTime);
                    dateTimeList.Add(dateTimeMinusOne);
                    break;
                }
                Thread.Sleep(100);
            }
            return dateTimeList;
        }

    }
}
