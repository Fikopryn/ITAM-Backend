using Domain.R3Framework.R3DataManagement;

namespace Domain.R3Framework.R3OAuth
{
    public class R3OAuthLogin
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string grant_type { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string refresh_token { get; set; }
        public string sysname { get; set; }
    }

    public class R3User
    {
        public string name { get; set; }
        public string email { get; set; }
        public string username { get; set; }
    }
    public class R3OauthLoginResponse
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public int refresh_expires_in { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public string session_state { get; set; }
        public string scoope { get; set; }
        public R3User users { get; set; }
        public string func { get; set; }
        public string subfunc { get; set; }
        public string posname { get; set; }
        public List<R3AppRole> Role { get; set; }
    }
    public class R3OAuthRealm
    {
        public List<string> roles { get; set; }
    }
    public class R3OAuthResource
    {
        public R3OAuthRealm account { get; set; }
    }
    public class R3OAuthIntrospect
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string token { get; set; }
    }
    //public class R3OAuthIntrospectResponse
    //{
    //    public int exp { get; set; }
    //    public int iat { get; set; }
    //    public string jti { get; set; }
    //    public string iss { get; set; }
    //    public string aud { get; set; }
    //    public string sub { get; set; }
    //    public string typ { get; set; }
    //    public string azp { get; set; }
    //    public string session_state { get; set; }
    //    public string name { get; set; }
    //    public string given_name { get; set; }
    //    public string family_name { get; set; }
    //    public string preferred_username { get; set; }
    //    public string email { get; set; }
    //    public string email_verified { get; set; }
    //    public string acr { get; set; }
    //    public R3OAuthRealm realm_access { get; set; }
    //    public R3OAuthResource resource_access { get; set; }
    //    public string scope { get; set; }
    //    public string sid { get; set; }
    //    public string client_id { get; set; }
    //    public string Username { get; set; }
    //    public string Token { get; set; }
    //    public string impersonateas { get; set; }
    //    public bool active { get; set; }
    //}
    public class R3OAuthLogout
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
    }
}
