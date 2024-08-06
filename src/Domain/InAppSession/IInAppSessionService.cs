using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InAppSession
{
    public interface IInAppSessionService
    {
        Task<Result<InAppSessionDto>> Save(InAppSessionDto inAppSessionDto);
        Task<Result<InAppSessionDto>> FindByRefreshToken(string refreshToken);
        Task<Result<InAppSessionDto>> FindByIpAddress(string ipAddress);
    }
}
