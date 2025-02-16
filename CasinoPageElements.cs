using OpenQA.Selenium;

namespace Automation.CasinoTests
{
    public class CasinoPageElements
    {
        public const string HomePage = "#root > div > div:nth-child(1) > div > div._content_1c1ma_36 > div > div.home__hero-header > div.app-logo-wrapper";
        public const string LoginUserNameByEmail = "input[data-testid='email-input']";
        public const string LoginPassword = "input[data-testid='password-input']";
        public const string LoginIconButtonInTheMain = "button[data-testid='lobby-login-btn']";
        public const string LoginAfterVerification = "button.button.button--main._logInButton_77srx_17";
        public const string MenuButtonAfterLogin = "#root > div._header_7jhnn_27 > div._top_7jhnn_39 > div._menuToggleWrapper_7jhnn_101 > button";
        public const string MyAccountButton = "ul.side-menu__links > li:nth-child(1) > button";
        public const string DialogPage = "#root > div._overlay_1v2z7_1.dialog-overlay.full-page-dialog__overlay > div.dialog-wrapper > div";
        public const string PenEditButton = "svg._pen_a31cg_31";
        public const string DialogPageAfterEdit = "#root > div._editProfileDialog_nil5y_1 > div > div.dialog-wrapper._dialog_nil5y_1 > div";
        public const string UserNameTextField = "#root div._nicknameInput_nil5y_9 div._inputWrapper_nil5y_13 input";
        public const string SelectAvatar = "div[data-testid^='avatar-image-']";
        public const string ApplyButton = "._btnContainer_nil5y_111 button > div";
        public const string CloseMyProfileButton = "#root > div._profileInfoDialog_kvois_1 > div > div.dialog-wrapper._dialog_kvois_1 > div > div.dialog__header.dialog__header-with-title > button";
        public const string SwitcherCoin = "[data-testid='coin-switcher']";
        public const string MyProfileButton = "div.account__content > div:nth-child(2) > button > div";
        public static By InvalidLoginErrorMessage = By.XPath("//div[contains(@class, '_errorMessage_12sog_1') and contains(@class, 'undefined') and text()='User not found. Did you mean to Sign Up?']");
    }
}
