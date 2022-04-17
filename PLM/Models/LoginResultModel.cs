namespace PLM.Models
{
    public class LoginResultModel
    {
        public string Access_token { get; set; }

        public string Expires_in { get; set; }

        public string Model { get; set; }

        public string OpenId { get; set; }

        public string Refresh_token { get; set; }

        public string Scope { get; set; }

        public string Token_type { get; set; }
    }
}
