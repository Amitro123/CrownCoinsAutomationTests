using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Automation.CasinoTests
{
    public class CasinoPage
    {
        public IWebDriver driver;
        public static string LastGeneratedUsername { get; private set; }

        private LoginCredentials loginCredentials = new LoginCredentials(
            "watchdogstest02+11@sunfltd.com",
            "123456",
            "https://app.dev.crowncoinscasino.com/"
        );

        public string Url => loginCredentials.Url;

        public CasinoPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void CheckLoginIsSuccessful()
        {
            GenericMethods.WaitUntilElementIsDisplayed(CasinoPageElements.LoginIconButtonInTheMain);
            Login(loginCredentials.Url, loginCredentials.UserName, loginCredentials.Password);
        }

        public void OpenMyAccountPage()
        {
            VerifyAndClickMenuButton();
            VerifyAndClickMyAccountButton();
        }

        public void OpenProfileEditor()
        {
            EditProfileDialog();
            GenericMethods.WaitUntilElementIsDisplayed(CasinoPageElements.UserNameTextField);
        }

        public void UpdateProfileUsername()
        {
            string updatedUsername = GenericMethods.UpdateUsernameWithRandomString();
            SaveProfileChanges();
        }

        public void SaveProfileChanges()
        {
            GenericMethods.Click(CasinoPageElements.ApplyButton);
        }

        public void OpenProfilePage()
        {
            GenericMethods.Click(CasinoPageElements.MyProfileButton);
        }

        public static void Login(string url, string userName, string password)
        {
            GenericMethods.WaitUntilElementIsDisplayed(CasinoPageElements.LoginIconButtonInTheMain);
            GenericMethods.Click(CasinoPageElements.LoginIconButtonInTheMain);
            GenericMethods.WaitUntilElementIsDisplayed(CasinoPageElements.LoginUserNameByEmail);
            GenericMethods.SendKeys(CasinoPageElements.LoginUserNameByEmail, userName);
            GenericMethods.SendKeys(CasinoPageElements.LoginPassword, password);
            GenericMethods.Click(CasinoPageElements.LoginAfterVerification);
        }

        public void ValidateProfileUsernameChange(string usernameBefore, string usernameAfter)
        {
            GenericMethods.ClickMyProfile();
            var profileUsername = GenericMethods.GetProfileNickname();

            Console.WriteLine($"Username before change: {usernameBefore}");
            Console.WriteLine($"Username from profile: {profileUsername}");

            Assert.AreNotEqual(usernameBefore, profileUsername, "Username was not changed");
            Assert.AreEqual(usernameAfter, profileUsername, "Profile username does not match the updated username");
        }

        private void VerifyAndClickMenuButton()
        {
            GenericMethods.WaitUntilElementIsDisplayed(CasinoPageElements.MenuButtonAfterLogin);

            GenericMethods.Click(CasinoPageElements.MenuButtonAfterLogin);
        }
        public string GetUsernameBeforeChange()
        {
            GenericMethods.WaitUntilElementIsDisplayed(CasinoPageElements.UserNameTextField);
            return GenericMethods.GetUsername();
        }

        private void VerifyAndClickMyAccountButton()
        {
            GenericMethods.WaitUntilElementIsDisplayed(CasinoPageElements.MyAccountButton);
            GenericMethods.Click(CasinoPageElements.MyAccountButton);
        }

        public string EditProfileAndUpdateUsername()
        {
            string updatedUsername = GenericMethods.UpdateUsernameWithRandomString();
            return updatedUsername;
        }

        public void SelectRandomAvatar()
        {
            GenericMethods.ClickRandomAvatar();
        }

        public void CloseEditProfileDialog()
        {
            GenericMethods.Click(CasinoPageElements.CloseMyProfileButton);
            GenericMethods.PressEscapeKey();
        }

        public void ClickOnApplyButton()
        {
            GenericMethods.Click(CasinoPageElements.ApplyButton);
        }
        public void ClickOnMyprofile()
        {
            GenericMethods.Click(CasinoPageElements.MyProfileButton);
        }

        public void EditProfileDialog()
        {
            GenericMethods.WaitUntilElementIsDisplayed(CasinoPageElements.DialogPage);
            GenericMethods.Click(CasinoPageElements.PenEditButton);
        }

        public void UseSwitcherCoinAndGetAmountOfCoins()
        {
            GenericMethods.WaitUntilElementIsDisplayed(CasinoPageElements.SwitcherCoin);

            var amountOfCoins = GenericMethods.GetBalance();

            GenericMethods.ClickAndDragRight(CasinoPageElements.SwitcherCoin);

            var amountOfCoinsAfterSwitcher = GenericMethods.GetBalance();

            Console.WriteLine($"Balance before switch: {amountOfCoins}");
            Console.WriteLine($"Balance after switch: {amountOfCoinsAfterSwitcher}");

            Assert.AreNotEqual(amountOfCoins, amountOfCoinsAfterSwitcher, "Balance did not change after switching!");
        }

    }
}

