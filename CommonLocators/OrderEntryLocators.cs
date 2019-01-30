using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaPoint_QA.CommonLocators
{
    public class OrderEntryLocators
    {
        public By placeBuyOrderButton = By.XPath("//button[text()='Place Buy Order']");
        public By placeSellOrderButton = By.XPath("//button[text()='Place Sell Order']");
        public By stopOrderTypeButton = By.XPath("//label[@data-test='Stop Order Type']");
        public By buyAmountTextField = By.XPath("//input[@data-test='Buy Amount']");
        public By sellAmountTextField = By.XPath("//input[@data-test='Sell Amount']");
        public By marketOrderTypeButton = By.XPath("//label[@data-test='Market Order Type']");
        public By limitOrderTypeButton = By.XPath("//label[@data-test='Limit Order Type']");
        public By buyOrderEntryButton = By.XPath("//label[@data-test='Buy Side']");
        public By sellOrderEntryButton = By.XPath("//label[@data-test='Sell Side']");
        public By orderEntryButton = By.XPath("//div[@data-test='Order Entry']");
        public By feesText = By.XPath("//*[@id='root']/div[1]/div[2]/div[2]/div[4]/div/div[2]/div/form/div[2]/div[3]/div[2]/span");
        public By orderTotalText = By.XPath("//*[@id='root']/div[1]/div[2]/div[2]/div[4]/div/div[2]/div/form/div[2]/div[3]/div[3]/span");
        public By netText = By.XPath("//*[@id='root']/div[1]/div[2]/div[2]/div[4]/div/div[2]/div/form/div[2]/div[3]/div[4]/span");
    }
}
