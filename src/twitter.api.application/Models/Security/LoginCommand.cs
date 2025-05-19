namespace twitter.api.application.Models.Security
{
    public class LoginCommand
    {
        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
