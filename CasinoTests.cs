using Automation.CasinoTests;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Automation.Clipro.CasinoTests
{
    [TestFixture]
    public class CasinoTests : CasinoTestBase
    {
        private CasinoPage Casino;
        private GenericMethods genericMethods;

        [SetUp]
        public void SetUp()
        {
            Casino = new CasinoPage(Driver);
        }

        [Test]
        [TestCase(TestName = "LoginIsSuccessful")]
        public void CheckLogin()
        {
            Casino.CheckLoginIsSuccessful();

            bool isLoginSuccessful = GenericMethods.WaitUntilElementIsVisable(By.CssSelector(CasinoPageElements.MenuButtonAfterLogin));

            Assert.IsTrue(isLoginSuccessful, "Login was not successful, Menu button is not visible.");
        }

        [TestCase("wrongEmail@example.com", "wrongPassword", TestName = "Test Invalid Login with wrong email and password")]
        [TestCase("anotherWrongEmail@example.com", "anotherWrongPassword", TestName = "Test Invalid Login with another wrong email and password")]
        public void CheckLoginWithInvalidCredentials(string username, string password, string url)
        {
            CasinoPage.Login(url, username, password);
            bool isErrorDisplayed = GenericMethods.WaitUntilElementIsVisable(CasinoPageElements.InvalidLoginErrorMessage);
            Assert.IsTrue(isErrorDisplayed, "Error message was not displayed for invalid login.");
        }

        [TestCase(TestName = "E2E_FullUserFlow")]
        public void TestLoginAndEditProfile()
        {
            Casino.CheckLoginIsSuccessful();
            Casino.OpenMyAccountPage();
            Casino.EditProfileDialog();
            var userNameBefore = Casino.GetUsernameBeforeChange();
            var userNameAfter = Casino.EditProfileAndUpdateUsername();
            Casino.SelectRandomAvatar();
            Casino.ClickOnApplyButton();
            Casino.ClickOnMyprofile();
            Casino.ValidateProfileUsernameChange(userNameBefore, userNameAfter);
            Casino.CloseEditProfileDialog();
            Casino.UseSwitcherCoinAndGetAmountOfCoins();
        }
    }
}
