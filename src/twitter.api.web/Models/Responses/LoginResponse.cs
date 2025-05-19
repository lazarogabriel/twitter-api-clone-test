namespace twitter.api.web.Models.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }

        public int ExpiresIn { get; set; }
    }
}
