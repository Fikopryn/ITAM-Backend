using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models.Entities.Tables;
using FluentResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Domain.Example.Table
{
    public class TableService : ITableService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TableService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<ExPersonReadDto>> GetById(int id)
        {
            try
            {
                var repoResult = await _uow.ExPersons.Set()
                    .Include(m => m.PersonContact).ThenInclude(m => m.PersonIds)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<ExPersonReadDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<IEnumerable<ExPersonReadDto>>> GetAllData()
        {
            try
            {
                var repoResult = await _uow.ExPersons.Set()
                    .Include(m => m.PersonContact).ThenInclude(m => m.PersonIds)
                    .ToListAsync();

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<IEnumerable<ExPersonReadDto>>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<ExPersonReadDto>> Create(ExPersonInsertDto data)
        {
            try
            {
                var dataToAdd = _mapper.Map<TExPerson>(data);
                dataToAdd.PersonContact = _mapper.Map<TExPersonContact>(data);

                await _uow.ExPersons.Add(dataToAdd);
                await _uow.CompleteAsync();

                var result = _mapper.Map<ExPersonReadDto>(dataToAdd);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<ExPersonIdReadDto>> CreateDetail(int headerId, ExPersonIdInsertDto data)
        {
            try
            {
                var repoResult = await _uow.ExPersons.Set()
                    .Include(m => m.PersonContact)
                    .FirstOrDefaultAsync(m => m.Id == headerId);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var dataToAdd = new TExPersonIdentification()
                {
                    PersonContactId = repoResult.PersonContact.Id,
                    IDType = data.IDType,
                    IDValue = data.IDValue
                };

                await _uow.ExPersonIdentifications.Add(dataToAdd);
                await _uow.CompleteAsync();

                var result = _mapper.Map<ExPersonIdReadDto>(dataToAdd);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<ExPersonReadDto>> Update(ExPersonReadDto data)
        {
            try
            {
                var repoResult = await _uow.ExPersons.Set()
                    .FirstOrDefaultAsync(m => m.Id == data.Id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                repoResult.Name = data.Name;
                repoResult.Surname = data.Surname;
                repoResult.Email = data.Email;
                repoResult.Street = data.Street;
                repoResult.City = data.City;

                _uow.ExPersons.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<ExPersonReadDto>(repoResult);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<ExPersonReadDto>> UpdateSpecific(int id, string email)
        {
            try
            {
                var repoResult = await _uow.ExPersons.Set()
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                repoResult.Email = email;

                _uow.ExPersons.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<ExPersonReadDto>(repoResult);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<ExPersonReadDto>> Delete(int id)
        {
            try
            {
                // NEED ADD INCLUDE BECAUSE HAVE CHILD FOREIGN KEY
                var repoResult = await _uow.ExPersons.Set()
                    .Include(m => m.PersonContact).ThenInclude(m => m.PersonIds)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _uow.ExPersons.Remove(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<ExPersonReadDto>(repoResult);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<ExPersonIdReadDto>> DeleteDetail(int detailId)
        {
            try
            {
                // NEED ADD INCLUDE BECAUSE HAVE CHILD FOREIGN KEY
                var repoResult = await _uow.ExPersonIdentifications.Set()
                    .FirstOrDefaultAsync(m => m.Id == detailId);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _uow.ExPersonIdentifications.Remove(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<ExPersonIdReadDto>(repoResult);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result> RunStoredProcedureExample()
        {
            try
            {
                var sql0 = "EXEC dbo.SP_EXAMPLE;";
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Name", "Dyana"));
                parameters.Add(new SqlParameter("@Surname", "Casacchia"));
                parameters.Add(new SqlParameter("@Email", "Dyana.Casacchia@legoo.com"));
                await _uow.ExecRawSqlAsync(sql0, parameters);
                await _uow.CompleteAsync();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog("ERROR", "RunStoredProcedureExample", SystemHelper.GetActualAsyncMethodName(), ex.GetMessage()), null);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
    }
}
