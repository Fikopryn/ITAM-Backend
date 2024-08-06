using Core.Models.Entities.Tables;
using Domain.R3Framework.R3User;
using FluentResults;
using Microsoft.AspNetCore.Http;

namespace Domain.Example.FileProcessing
{
    public interface IFileProcessingService
    {
        Task<Result<ExFileReferenceDto>> UploadFile(IFormFile fileData, string modulId, string moduleName, string fileCategory);
        Task<Result<List<ExFileReferenceDto>>> UploadMultiFile(List<IFormFile> fileData, string modulId, string moduleName, string fileCategory);
        Task<Result<List<ExFileReferenceDto>>> GetFileList(ExFileReferenceDto param);
        Task<Result<ExFileReferenceDto>> GetFile(ExFileReferenceDto param);
        Task<Result<UploadResult>> ExcelUploader(string appCodeName, string tableName, DateTime uploadDateTime, IFormFile file, R3UserSession userSession, string modulUid, string moduleCode);
        Task<Result<ExcelProcessingResult>> ExcelProcessing(string uploadBy, IFormFile file);
        Task<Result<string>> DeleteFile(ExFileReferenceDto param);
        Task<Result<string>> DeleteMultiFile(List<ExFileReferenceDto> param);
        Task<Result<UploadResult>> SaveInterfaceToDb(string modulUid, R3UserSession userSession, string moduleCode);
    }
}
