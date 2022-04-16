namespace PLM.Models
{
    public class LoginResultModel
    {
        public string AccessToken { get; set; }

        public string ExpiresIn { get; set; }

        public string Model { get; set; }

        public string OpenId { get; set; }

        public string RefreshToken { get; set; }

        public string Scope { get; set; }

        public string TokenType { get; set; }
    }
}
