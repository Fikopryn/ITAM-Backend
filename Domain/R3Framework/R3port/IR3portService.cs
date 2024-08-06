using Domain.R3Framework.R3DataManagement;
using Domain.R3Framework.R3User;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.R3Framework.R3port
{
    public interface IR3portService
    {
        Task<Result<R3portOutputDto>> GetReportFile(R3UserSession userAction, R3portInputDto reportParam);
    }
}
