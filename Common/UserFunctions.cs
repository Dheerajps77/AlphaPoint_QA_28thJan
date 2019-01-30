using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using AlphaPoint_QA.Pages;
using AlphaPoint_QA.Utils;
using log4net;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Common
{

    public class UserFunctions
    {
        static ILog logger;
        private readonly ITestOutputHelper output;
        static Config data;
        public static IWebDriver driver;
        static string username;
        static string password;

        public UserFunctions(ITestOutputHelper output)
        {
            this.output = output;
            logger = APLogger.GetLog();
            logger.Info("Test Started");
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        By selectServer = By.XPath("//select[@name='tradingServer']");
        By userLoginName = By.XPath("//input[@name='username']");
        By userLoginPassword = By.XPath("//input[@name='password']");
        By userLoginButton = By.XPath("//button[text()='Log In']");
        By loggedInUserName = By.XPath("//button[@class='user-summary__popover-menu-trigger page-header-user-summary__popover-menu-trigger']");
        By userSignOutButton = By.XPath("//span[contains(@class,'popover-menu__item-label') and text()='Sign Out']");

        //This method is used for User Login
        public void LogIn(ILog logger = null)
        {
            try
            {
                username = data.UserPortal.Users["User1"].UserName;
                password = data.UserPortal.Users["User1"].Password;
                string userUrl = data.UserPortal.PortalUrl;
                string userServerName = data.UserPortal.PortalServerUrl;

                driver.Navigate().GoToUrl(userUrl);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                driver.Manage().Window.Size = new Size(1366, 768);
                IWebElement serverWebElement = driver.FindElement(selectServer);

                UserSetFunctions.SelectDropdown(serverWebElement, userServerName);
                UserSetFunctions.EnterText(driver.FindElement(userLoginName), username);
                UserSetFunctions.EnterText(driver.FindElement(userLoginPassword), password);
                UserSetFunctions.Click(driver.FindElement(userLoginButton));
                Assert.Equal(driver.Title.ToLower(), "APEX Web".ToLower());
               // logger.Info("User "+ username +" logged in successfully");
            }
            catch (Exception e)
            {
                logger.Error("Login failed");
                logger.Error(e.StackTrace);
                throw e;
            }
        }

        //This method is used for User Logout
        public void LogOut()
        {
            try
            {
                Thread.Sleep(2000);
                UserCommonFunctions.ScrollingUpVertical(driver);
                UserSetFunctions.Click(driver.FindElement(loggedInUserName));
                UserSetFunctions.Click(driver.FindElement(userSignOutButton));
                logger.Info("User " + username + " logged out successfully");
            }
            catch (Exception e)
            {
                logger.Error("Logout failed");
                logger.Error(e.StackTrace);
                throw e;
            }
        }
    }
}