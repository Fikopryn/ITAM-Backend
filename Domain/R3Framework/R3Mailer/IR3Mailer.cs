using Domain.R3Framework.R3User;
using FluentResults;

namespace Domain.R3Framework.R3Mailer
{
    public interface IR3Mailer
    {
        Task<Result<MailerResponse>> SendEmail(R3UserSession userAction, MailerPayload emailPayload);
    }
}
