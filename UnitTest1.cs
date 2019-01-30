using AlphaPoint_QA.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace AlphaPoint_QA
{
    public class UnitTest1
    {
        private static IWebDriver driver;
        //static String productUrl = SeleniumUtil.fetchUserDetails("PRODUCT_URL");

        public UnitTest1()
        {
            var os = System.Environment.OSVersion.Platform;

        }


        [Fact]
        public void Test1()
        {

        }
    }
}
