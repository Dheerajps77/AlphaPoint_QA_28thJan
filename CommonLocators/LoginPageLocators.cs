using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaPoint_QA.CommonLocators
{
    class LoginPageLocators
    {

        //Below are the Normal user portal details
        By userName = By.XPath("//input[@name='username']");
        By passWord = By.XPath("//input[@name='password']");
        By loginInButton = By.XPath("//button[text()='Log In']");
        By selectServerfromList = By.XPath("//select[@name='tradingServer']");
        
        //Below are the Admin portal details
        By adminUsername = By.XPath("//input[@placeholder='Username']");
        By adminPassword = By.XPath("//input[@placeholder='Password']");
        By adminLoginButton = By.XPath("//button[@id='login-btn']");
        By openUserMenu = By.XPath("//button[@id='OpenUserMenu']");
        By adminSignOut = By.XPath("//*[@id='SignOut']/div/div/div");
        By adminUsertext = By.XPath("//span[text()='Users']");
    }
}
