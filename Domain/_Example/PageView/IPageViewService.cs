using Core.Models;
using FluentResults;

namespace Domain.Example.PageView
{
    public interface IPageViewService
    {
        Task<Result<PagingResponse<ExPersonViewDto>>> GetPageData(PagingRequest pRequest);
    }
}
