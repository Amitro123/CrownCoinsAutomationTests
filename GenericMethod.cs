using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Automation.CasinoTests
{
    public class GenericMethods
    {
        [ThreadStatic]
        private static WebDriverWait _wait;

        private static IWebDriver Driver => BaseDrivers.Driver;
        private static readonly Random _random = new Random();
        private static readonly TimeSpan _timeout = TimeSpan.FromSeconds(10);

        #region Initialization and Navigation// remmber to remove this

        public static void InitializeBaseDrivers()
        {
            if (Driver == null)
            {
                throw new Exception("WebDriver is not initialized. Ensure BaseDrivers is set up before calling this method.");
            }

            _wait = new WebDriverWait(Driver, _timeout);
        }

        public static void RetryPageLoad()
        {
            if (Driver == null)
            {
                throw new Exception("Cannot refresh the page because WebDriver is not initialized.");
            }

            Driver.Navigate().Refresh();
        }

        public static string UpdateUsernameWithRandomString()
        {
            string randomUsername = string.Empty;

            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                IWebElement usernameInput = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                    By.CssSelector(CasinoPageElements.UserNameTextField)));

                Thread.Sleep(400);

                usernameInput.Clear();

                randomUsername = GenerateRandomUsername();
                usernameInput.SendKeys(randomUsername);

                Console.WriteLine($"Updated username to: {randomUsername}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating the username: " + ex.Message);
            }

            return randomUsername;
        }

        public static string GetProfileNickname()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                IWebElement nicknameElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    By.CssSelector("div._nicknameText_kvois_10[data-testid='my-profile-nickname']")));

                string nickname = nicknameElement.Text;
                Console.WriteLine($"Retrieved nickname: {nickname}");
                return nickname;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving the nickname: " + ex.Message);
                return string.Empty;
            }
        }
        public static string GetBalance()
        {
            try
            {
                IWebElement balanceElement = Driver.FindElement(By.CssSelector("[data-testid='lobby-balance-bar']"));

                return balanceElement.Text;
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Balance element not found.");
                return "Balance not found";
            }
        }

        public static void PressEscapeKey()
        {
            try
            {
                Actions action = new Actions(Driver);
                action.SendKeys(Keys.Escape).Perform();
                Console.WriteLine("Esc key pressed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while pressing the Esc key: " + ex.Message);
            }
        }

        public static bool WaitUntilElementIsVisable(By elementLocator)
        {
            try
            {
                if (_wait == null)
                {
                    InitializeBaseDrivers();
                }

                var element = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(elementLocator));
                return element != null;
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"Element with locator {elementLocator} was not visible after waiting for {_timeout.TotalSeconds} seconds.");
                return false;
            }
        }

        private static string GenerateRandomUsername()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return "User" + new string(Enumerable.Range(0, 6).Select(_ => chars[_random.Next(chars.Length)]).ToArray());
        }

        public static void GoToUrl(string url)//should handle this
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new Exception("GoToUrl failed: URL is null or empty.");
            }

            Driver.Navigate().GoToUrl(url);
            Driver.Manage().Window.Maximize();
        }


        public static void Click(string cssSelector)
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(cssSelector))).Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception($"The element with the css {cssSelector} was not found. " + ex.Message);
            }
        }

        public static void ClickAndDragRight(string css)
        {
            _wait.Until(ExpectedConditions.ElementExists(By.CssSelector(css))).Click();
            IWebElement element = Driver.FindElement(By.CssSelector(css));
            Actions builder = new Actions(Driver);
            builder.ClickAndHold(element).MoveByOffset(100, 0).Release().Perform();
        }

        public static void SendKeys(string cssSelector, string text)
        {
            try
            {
                IWebElement element = _wait.Until(ExpectedConditions.ElementExists(By.CssSelector(cssSelector)));
                element.Clear();
                element.SendKeys(text);
            }
            catch (Exception ex)
            {
                throw new Exception($"Method SendKeys failed css: {cssSelector} text: {text}, {ex.Message}");
            }
        }

        public static void WaitUntilElementIsDisplayed(string css, double waitSecondsBefore = 0, double waitSecondsAfter = 0, double wait = 30)
        {
            _wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(wait));
            Thread.Sleep(TimeSpan.FromSeconds(waitSecondsBefore));
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(css)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Waiting by CSS  : '{css}' failed , Exception : {ex.ToString()}");
            }

            Thread.Sleep(TimeSpan.FromSeconds(waitSecondsAfter));
        }

        public static string GetUsername()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                IWebElement usernameInput = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    By.CssSelector(CasinoPageElements.UserNameTextField)));

                string username = usernameInput.GetAttribute("value");
                Console.WriteLine($"Retrieved username: {username}");
                return username;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving the username: " + ex.Message);
                return string.Empty;
            }
        }

        public static void ClickRandomAvatar()
        {
            var avatars = GetWebElementsWithoutWait(CasinoPageElements.SelectAvatar);

            if (avatars.Count == 0)
            {
                throw new Exception("No avatars found on the page.");
            }

            int randomIndex = _random.Next(avatars.Count);

            avatars[randomIndex].Click();

            Console.WriteLine($"Selected Avatar Index: {randomIndex}");
        }

        public static void ClickApplyButton(IWebDriver driver)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement applyButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                    By.CssSelector(CasinoPageElements.ApplyButton)));

                applyButton.Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while clicking the Apply button: " + ex.Message);
            }
        }

        public static void ClickMyProfile()
        {
            try
            {
                var accountButton = Driver.FindElement(By.CssSelector(CasinoPageElements.MyProfileButton));
                accountButton.Click();
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("MyProfile button not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clicking MyProfile button: {ex.Message}");
            }
        }

        public static IList<IWebElement> GetWebElementsWithoutWait(string cssSelector)
        {
            IList<IWebElement> elements = Driver.FindElements(By.CssSelector(cssSelector));
            return elements;
        }

        #endregion Element Retrieval and Get Actions// remmber to remove this
    }

}
