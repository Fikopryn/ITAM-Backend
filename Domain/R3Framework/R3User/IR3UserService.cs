using FluentResults;

namespace Domain.R3Framework.R3User
{
    public interface IR3UserService
    {
        Task<Result<R3UserAuth>> AuthLogin(R3UserLogin data);
        Task<Result<R3UserLogout>> AuthLogout(R3UserSession userAction);

        Task<Result<R3UserSession>> GetUserSession(string token);
        Task<Result<R3UserSession>> GetUserSession(R3UserSession userAction);
        Task<Result<bool>> TokenValidation(R3UserSession userAction);
    }
}
