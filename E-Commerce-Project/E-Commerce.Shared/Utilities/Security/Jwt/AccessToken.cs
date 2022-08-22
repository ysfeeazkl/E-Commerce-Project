namespace E_Commerce.Shared.Utilities.Security.Jwt
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
