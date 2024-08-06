using Domain.R3Framework.R3OAuth;

namespace Domain.R3Framework.R3User
{
    public class R3UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class R3UserImpersonate
    {
        public string UserId { get; set; }
    }

    public class R3UserLogout
    {
        public string Message { get; set; }
    }

    public class R3UserAuth
    {
        public string Access_token { get; set; }
        public string Token_type { get; set; }
        public string Expires_at { get; set; }
        public R3UserSession Users { get; set; }
        public string Subfunc { get; set; }
        public string Posname { get; set; }
        public List<object> Role { get; set; }
    }

    public class R3UserSession
    {
        public int exp { get; set; }
        public int iat { get; set; }
        public string jti { get; set; }
        public string iss { get; set; }
        public string aud { get; set; }
        public string sub { get; set; }
        public string typ { get; set; }
        public string azp { get; set; }
        public string session_state { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string preferred_username { get; set; }
        public string email { get; set; }
        public string email_verified { get; set; }
        public string acr { get; set; }
        public R3OAuthRealm realm_access { get; set; }
        public R3OAuthResource resource_access { get; set; }
        public string scope { get; set; }
        public string sid { get; set; }
        public string client_id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string ImpersonateAs { get; set; }
        public bool active { get; set; }
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string Email { get; set; }
        //public object Email_verified_at { get; set; }
        //public DateTime Created_at { get; set; }
        //public DateTime Updated_at { get; set; }
        //public string Guid { get; set; }
        //public string Domain { get; set; }
        //public string Username { get; set; }
        //public string Token { get; set; }
        //public string ImpersonateAs { get; set; }
    }
}
