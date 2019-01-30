using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using log4net;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Pages
{
    class UserSettingPage
    {
        IWebDriver driver;
        static ILog logger;
        private readonly ITestOutputHelper output;

        By apiKeysLink = By.XPath("//li[@data-test='API Keys']");
        By apiKeyButton = By.XPath("//button[@class='ap-button__btn ap-button__btn--general retail-api-keys-form__btn retail-api-keys-form__btn--general']");
        By tradingCheckbox = By.XPath("//input[@name='trading']");
        By depositsCheckbox = By.XPath("//input[@name='deposit']");
        By withdrawlsCheckbox = By.XPath("//input[@name='withdraw']");
        By createAPIKeyButton = By.XPath("/html/body/div[4]/div/div/div[2]/form/div[2]/div[2]/button");
        By apiConfirmation = By.XPath("/html/body/div[5]/div/div");
        By apiConfirmationPermissions = By.XPath("//span[@data-test='Permissions']");
        By apiConfirmationKey = By.XPath("//span[@data-test='Key']");
        By apiConfirmationSecret = By.XPath("//span[@data-test='Secret']");
        By deleteAPIKeyModalButton = By.XPath("//button[@class='ap-button__btn ap-button__btn--subtractive ap-modal retail-api-keys-modal-delete__btn ap-modal retail-api-keys-modal-delete__btn--subtractive']");
        By affliateProgramLink = By.XPath("//li[@data-test='Affiliate Program']");
        By noOfAffiliates = By.XPath("//section[@class='affiliate__body']/section[2]/section/p");
        By copyButtonLocator = By.XPath("//div[@class='affiliate__btn-container']/button[2]");
        By editButtonLocator = By.XPath("//div[@class='affiliate__btn-container']/button[1]");

        public UserSettingPage(IWebDriver driver, ITestOutputHelper output)
        {
            this.driver = driver;
            this.output = output;
            logger = APLogger.GetLog();
        }

        public IWebElement TradingCheckbox(IWebDriver driver)
        {
            return driver.FindElement(tradingCheckbox);
        }

        public IWebElement DepositsCheckbox(IWebDriver driver)
        {
            return driver.FindElement(tradingCheckbox);
        }

        public IWebElement WithdrawlsCheckbox(IWebDriver driver)
        {
            return driver.FindElement(tradingCheckbox);
        }

        public IWebElement APIKeysLink(IWebDriver driver)
        {
            return driver.FindElement(apiKeysLink);
        }

        public IWebElement AffiliateProgramLink(IWebDriver driver)
        {
            return driver.FindElement(affliateProgramLink);
        }

        public IWebElement APIKeyButton(IWebDriver driver)
        {
            return driver.FindElement(apiKeyButton);
        }

        public IWebElement CreateAPIKeyButton(IWebDriver driver)
        {
            return driver.FindElement(createAPIKeyButton);
        }

        public IWebElement APIConfirmationModal(IWebDriver driver)
        {
            return driver.FindElement(createAPIKeyButton);
        }

        public IWebElement APIConfirmationButton(IWebDriver driver)
        {
            return driver.FindElement(createAPIKeyButton);
        }

        public IWebElement APIConfirmationPermissions(IWebDriver driver)
        {
            return driver.FindElement(apiConfirmationPermissions);
        }

        public IWebElement APIConfirmationKey(IWebDriver driver)
        {
            return driver.FindElement(apiConfirmationKey);
        }

        public IWebElement APIConfirmationSecret(IWebDriver driver)
        {
            return driver.FindElement(apiConfirmationSecret);
        }

        public IWebElement DeleteAPIKeymodalButton(IWebDriver driver)
        {
            return driver.FindElement(deleteAPIKeyModalButton);
        }

        public IWebElement CopyAffiliateTagButton(IWebDriver driver)
        {
            return driver.FindElement(copyButtonLocator);
        }

        // This method Verifies API Key Checkboxes Are Present
        public bool VerifyAPIKeyCheckboxesArePresent(IWebDriver driver)
        {
            if (TradingCheckbox(driver).Displayed && DepositsCheckbox(driver).Displayed && WithdrawlsCheckbox(driver).Displayed)
            {
                logger.Info("The 3 checkboxes tradingCheckbox, depositsCheckbox and withdrawlsCheckbox are present");
                return true;
            }
            else
            {
                return false;
            }
        }

        // This method stores the data present in the API Confirmation Modal
        public Dictionary<string, string> StoreAPIConfirmationModalData(IWebDriver driver)
        {
            Dictionary<string, string> apiConfirmationModalData = new Dictionary<string, string>();
            apiConfirmationModalData.Add("Permissions", APIConfirmationPermissions(driver).Text);
            apiConfirmationModalData.Add("Key", APIConfirmationKey(driver).Text);
            apiConfirmationModalData.Add("Secret", APIConfirmationSecret(driver).Text);
            return apiConfirmationModalData;
        }

        // This method stores the list of activities checked
        public Dictionary<string, bool> StoreAPICheckedActivitiesData(IWebDriver driver)
        {
            Dictionary<string, bool> checkedActivitiesData = new Dictionary<string, bool>();
            checkedActivitiesData.Add("Trading", APIConfirmationPermissions(driver).Selected);
            checkedActivitiesData.Add("Deposits", APIConfirmationKey(driver).Selected);
            checkedActivitiesData.Add("Withdrawls", APIConfirmationSecret(driver).Selected);
            return checkedActivitiesData;
        }

        // This method Deletes the API key, takes bool deleteFlag true
        // Returns true if the API Key is successfully deleted
        public bool VerifyDeleteButtonIsPresent(IWebDriver driver, Dictionary<string, string> apiKeyData, bool deleteFlag)
        {
            ArrayList apiKeysList = new ArrayList();
            string apiKeyAdded = apiKeyData["Key"];
            var flag = false;
            int countOfAPIKeys = driver.FindElements(By.XPath("//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div")).Count;
            for (int i = 1; i <= countOfAPIKeys; i++)
            {
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div")).Count;
                for (int j = 1; j <= countItems; j++)
                {
                    string apiKey = driver.FindElement(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div[1]")).Text;
                    if (apiKey.Equals(apiKeyAdded))
                    {
                        IWebElement deleteButton = driver.FindElement(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div[5]"));
                        if (deleteButton.Text.Equals("Delete"))
                        {
                            if (deleteFlag)
                            {
                                deleteButton.Click();
                                UserSetFunctions.Click(DeleteAPIKeymodalButton(driver));
                                flag = true;
                            }

                        }

                    }

                }
            }
            return flag;
        }

        // This method verifies the Deleted Key is not present in the List
        // Returns True if Deleted Key is not displayed
        public bool VerifyAPIKeyIsDeleted(IWebDriver driver, Dictionary<string, string> apiKeyData)
        {
            ArrayList apiKeysList = new ArrayList();
            string apiKeyAdded = apiKeyData["Key"];
            var flag = false;
            int countOfAPIKeys = driver.FindElements(By.XPath("//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div")).Count;
            for (int i = 1; i <= countOfAPIKeys; i++)
            {
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div")).Count;
                for (int j = 1; j <= countItems; j++)
                {
                    string apiKey = driver.FindElement(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div[1]")).Text;
                    if (apiKey.Equals(apiKeyAdded))
                    {
                        flag = true;
                    }

                }
            }
            return flag;
        }

        // This method verifies if the Secret key is present in the List
        // Secret key should not be displayed
        // Returns True if Secret Key is not displayed
        public bool VerifySecretkeyIsPresent(IWebDriver driver, Dictionary<string, string> apiKeyData)
        {
            var flag = false;
            string secretKeyAdded = apiKeyData["Secret"];
            int countOfAPIKeys = driver.FindElements(By.XPath("//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div")).Count;

            for (int i = 1; i <= countOfAPIKeys; i++)
            {
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div")).Count;
                for (int j = 1; j <= countItems; j++)
                {
                    string verifySecretKey = driver.FindElement(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div[1]")).Text;
                    if (verifySecretKey.Equals(secretKeyAdded))
                    {
                        flag = true;
                        logger.Error("Verification failed - Secret Key is displayed");
                    }

                }
            }
            return flag;
        }

        // This method verifies if the API key added is present in the List
        // This method matches the API key activities on List versus the activities selected while creating an API Key 
        // Returns True if success
        public bool VerifyAddedAPIKey(IWebDriver driver, Dictionary<string, string> apiKeyData)
        {
            ArrayList apiKeysList = new ArrayList();
            string apiKeyAdded = apiKeyData["Key"];
            var flag = false;
            Dictionary<string, bool> checkedActivitiesAdded = StoreAPICheckedActivitiesData(driver);
            bool allowDeposits = checkedActivitiesAdded["Deposits"];
            bool allowTradings = checkedActivitiesAdded["Trading"];
            bool allowWithdrawls = checkedActivitiesAdded["Withdrawls"];

            int countOfAPIKeys = driver.FindElements(By.XPath("//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div")).Count;
            for (int i = 1; i <= countOfAPIKeys; i++)
            {
                int countItems = driver.FindElements(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div")).Count;
                for (int j = 1; j <= countItems; j++)
                {
                    string apiKey = driver.FindElement(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div[1]")).Text;
                    string deleteButton = driver.FindElement(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div[5]")).Text;

                    if (apiKey.Equals(apiKeyAdded))
                    {
                        // This matches the state of the items checked
                        bool allowDepositsState = allowDeposits.Equals(driver.FindElement(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div[2]")).Enabled);
                        bool allowWithdrawlsState = allowWithdrawls.Equals(driver.FindElement(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div[3]")).Enabled);
                        bool allowTradingsState = allowTradings.Equals(driver.FindElement(By.XPath("(//div[@class='flex-table__body api-key-list__body retail-api-key-list__body']/div)[" + i + "]/div[4]")).Enabled);

                        if (allowDepositsState && allowWithdrawlsState && allowTradingsState)
                        {
                            if (deleteButton.Equals("Delete"))
                            {
                                if (!VerifySecretkeyIsPresent(driver, apiKeyData))
                                {
                                    flag = true;
                                    logger.Info("API key added is present in the List");
                                    logger.Info("API key activities matched");
                                }

                            }

                        }
                    }
                }
            }
            return flag;
        }

        // This method Creates an API Key, returns API KEY DATA Stored in API Confirmation Modal
        public Dictionary<string, string> CreateAPIkey(IWebDriver driver)
        {

            Dictionary<string, string> apiKeyData = new Dictionary<string, string>();

            UserCommonFunctions.DashBoardMenuButton(driver);
            UserCommonFunctions.NavigateToUserSetting(driver);

            IWebElement apiKeysLink = APIKeysLink(driver);
            IWebElement apiKeyBtn = APIKeyButton(driver);
            // Verify the button is enabled
            if (apiKeyBtn.Enabled)
            {
                UserSetFunctions.Click(apiKeyBtn);

                // Verify the 3 checkboxes are displayed
                if (VerifyAPIKeyCheckboxesArePresent(driver))
                {
                    UserSetFunctions.Click(TradingCheckbox(driver));
                    UserSetFunctions.Click(DepositsCheckbox(driver));
                    UserSetFunctions.Click(WithdrawlsCheckbox(driver));

                    // This method stores the list of activities checked
                    StoreAPICheckedActivitiesData(driver);


                    // This method creates the new API key
                    UserSetFunctions.Click(CreateAPIKeyButton(driver));

                    if (APIConfirmationModal(driver).Displayed)
                    {
                        apiKeyData = StoreAPIConfirmationModalData(driver);
                        UserSetFunctions.Click(APIConfirmationButton(driver));
                        Assert.True(VerifyAddedAPIKey(driver, apiKeyData));
                        return apiKeyData;
                    }

                }

            }
            else
            {
                logger.Error("API Key button is not enabled");
            }
            return apiKeyData;
        }

        // This method deletes the API Key
        // Returns true if Delete Button is clicked and API Key is deleted
        public bool DeleteAPIKey(IWebDriver driver)
        {
            var flag = false;
            var deleteFlag = true;

            Dictionary<string, string> apiKeyData = CreateAPIkey(driver);
            bool verifyDeleteButton = VerifyDeleteButtonIsPresent(driver, apiKeyData, deleteFlag);
            bool verifyAPIKeyIsDeleted = VerifyAPIKeyIsDeleted(driver, apiKeyData);

            if (verifyDeleteButton && verifyAPIKeyIsDeleted)
            {
                flag = true;
            }
            return flag;
        }


        // This method returns the number of affiliates
        public int GetnumberOfAffiliates(IWebDriver driver)
        {
            UserSetFunctions.Click(AffiliateProgramLink(driver));
            string affiliates = driver.FindElement(noOfAffiliates).Text;
            string[] affiliatesList = affiliates.Split(" ");
            affiliates = affiliatesList[0];
            return Int32.Parse(affiliates);
        }

        // This method verifies affiliates program
        public bool VerifyAffiliateProgramFunctionality(IWebDriver driver)
        {
            var flag = false;
            int affiliatesBefore = GetnumberOfAffiliates(driver);
            UserSetFunctions.Click(CopyAffiliateTagButton(driver));
            GenericUtils.CloseCurrentBrowserTab(driver);
            GenericUtils.OpenNewBrowserWindow(driver, OpenQA.Selenium.Keys.Control + "v");
            // Register a new user
            // Verify the user is registered successfully
            // Close previous browser and open new browser
            int affiliatesAfterRegUser = GetnumberOfAffiliates(driver);
            if ((affiliatesAfterRegUser - affiliatesBefore) == 1)
            {
                flag = true;
            }
            return flag;
        }
    }
}
