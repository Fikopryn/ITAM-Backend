using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Views;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.Example.PageView
{
    public class PageViewService : IPageViewService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PageViewService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<PagingResponse<ExPersonViewDto>>> GetPageData(PagingRequest pRequest)
        {
            try
            {
                Expression<Func<VwExPerson, bool>> _where = m => true;
                foreach (var parameter in pRequest.Parameters)
                {
                    if (!string.IsNullOrWhiteSpace(parameter.SearchValue))
                    {
                        var colData = parameter.Name.ToLower();

                        if (colData == nameof(ExPersonViewDto.Name).ToLower())
                        {
                            _where = _where.And(m => m.Name.ToLower().Contains(parameter.SearchValue.ToLower()));
                        }

                        if (colData == nameof(ExPersonViewDto.Surname).ToLower())
                        {
                            _where = _where.And(m => m.Surname.ToLower().Contains(parameter.SearchValue.ToLower()));
                        }

                        if (colData == nameof(ExPersonViewDto.Email).ToLower())
                        {
                            _where = _where.And(m => m.Email.ToLower().Contains(parameter.SearchValue.ToLower()));
                        }

                        if (colData == nameof(ExPersonViewDto.Street).ToLower())
                        {
                            _where = _where.And(m => m.Street.ToLower().Contains(parameter.SearchValue.ToLower()));
                        }

                        if (colData == nameof(ExPersonViewDto.City).ToLower())
                        {
                            _where = _where.And(m => m.City.ToLower().Contains(parameter.SearchValue.ToLower()));
                        }

                        if (colData == nameof(ExPersonViewDto.HomeNumber).ToLower())
                        {
                            _where = _where.And(m => m.HomeNumber.ToLower().Contains(parameter.SearchValue.ToLower()));
                        }

                        if (colData == nameof(ExPersonViewDto.PhoneNumber).ToLower())
                        {
                            _where = _where.And(m => m.PhoneNumber.ToLower().Contains(parameter.SearchValue.ToLower()));
                        }

                        if (colData == nameof(ExPersonViewDto.KTP).ToLower())
                        {
                            _where = _where.And(m => m.KTP.ToLower().Contains(parameter.SearchValue.ToLower()));
                        }

                        if (colData == nameof(ExPersonViewDto.NPWP).ToLower())
                        {
                            _where = _where.And(m => m.NPWP.ToLower().Contains(parameter.SearchValue.ToLower()));
                        }

                    }
                }
                var pageCond = new PagingCondition<VwExPerson>(_where, pRequest);

                var countAll = await _uow.VwExPersons.Set().CountAsync();
                var resultData = await _uow.VwExPersons.GetPagedData(pageCond);
                var resultCount = await _uow.VwExPersons.CountData(pageCond);

                var result = PagingResponse<ExPersonViewDto>.CreateResponse(resultCount, countAll, _mapper.Map<IEnumerable<ExPersonViewDto>>(resultData), pRequest.Page, pRequest.Length);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
    }
}
