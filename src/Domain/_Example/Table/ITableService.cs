using FluentResults;

namespace Domain.Example.Table
{
    public interface ITableService
    {
        Task<Result<ExPersonReadDto>> GetById(int id);
        Task<Result<IEnumerable<ExPersonReadDto>>> GetAllData();

        Task<Result<ExPersonReadDto>> Create(ExPersonInsertDto data);
        Task<Result<ExPersonIdReadDto>> CreateDetail(int headerId, ExPersonIdInsertDto data);
        Task<Result<ExPersonReadDto>> Update(ExPersonReadDto data);
        Task<Result<ExPersonReadDto>> UpdateSpecific(int id, string email);
        Task<Result<ExPersonReadDto>> Delete(int id);
        Task<Result<ExPersonIdReadDto>> DeleteDetail(int detailId);
    }
}
