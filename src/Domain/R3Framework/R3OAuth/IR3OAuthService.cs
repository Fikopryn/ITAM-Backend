using Domain.R3Framework.R3User;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.R3Framework.R3OAuth
{
    public interface IR3OAuthService
    {
        Task<Result<R3OauthLoginResponse>> Login(R3OAuthLogin r3OAuthLogin, string ipAddress, bool encoded = true);
        Task<Result<R3OauthLoginResponse>> RefreshToken(R3OAuthLogin r3OAuthLogin, string ipAddress);
        Task<Result<R3UserSession>> Introspect(R3OAuthIntrospect r3OAuthIntrospect);
        Task<Result<string>> Logout(R3OAuthLogout r3OAuthLogout);
    }
}
