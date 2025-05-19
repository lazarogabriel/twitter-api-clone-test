namespace twitter.api.application.Models.Security
{
    public class RegisterCommand
    {
        public RegisterCommand(string userName, string email, string password)
        {
            UserName = userName;
            Email = email;
            Password = password;
        }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
