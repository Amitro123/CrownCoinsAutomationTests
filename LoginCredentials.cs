namespace Automation.CasinoTests
{
    public class LoginCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }

        public LoginCredentials(string userName, string password, string url)
        {
            UserName = userName;
            Password = password;
            Url = url;
        }
    }

}
