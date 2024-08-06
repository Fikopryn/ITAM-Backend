using Core;
using Core.Extensions;
using Core.Helpers;
using Domain.R3Framework.R3User;
using FluentResults;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Core.Interfaces;
using Core.Models.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Newtonsoft.Json;
using Core.Models;
using Microsoft.Extensions.Options;
using System.IO.Pipelines;
using Domain.R3Framework.R3DataManagement;

namespace Domain.Example.FileProcessing
{
    public class FileProcessingService : IFileProcessingService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UploaderConfig _uploaderConfig;

        public FileProcessingService(
            IUnitOfWork uow,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IOptions<UploaderConfig> uploaderConfig)
        {
            _uow = uow;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _uploaderConfig = uploaderConfig.Value;
        }

        //DIGUNAKAN UNTUK INCREMENT JIKA NAMA FOLDER SAAT UPLOAD SAMA
        private string GetNextFilename(string pathFile)
        {
            int i = 1;
            string dir = Path.GetDirectoryName(pathFile);
            string file = Path.GetFileNameWithoutExtension(pathFile) + "{0}";
            string extension = Path.GetExtension(pathFile);

            while (File.Exists(pathFile))
                pathFile = Path.Combine(dir, string.Format(file, "(" + i++ + ")") + extension);

            return pathFile;
        }

        public async Task<Result<ExFileReferenceDto>> UploadFile(IFormFile fileData, string modulId, string moduleName, string fileCategory)
        {
            try
            {
                if (fileData.Length > 0)
                {
                    DateTime timeStamp = DateTime.Now;
                    string fileName = Path.GetFileName(fileData.FileName);
                    string dir = Path.Combine(_uploaderConfig.PathDirectory, moduleName, modulId);
                    Directory.CreateDirectory(dir);
                    Guid mdId = new Guid(modulId);

                    string uploadPath = Path.Combine(dir, fileName);
                    uploadPath = GetNextFilename(uploadPath);

                    string newFileName = Path.GetFileName(uploadPath);

                    var fileDetails = new TExFileReference()
                    {
                        ModulId = mdId,
                        ModulName = moduleName,
                        FileCategory = fileCategory,
                        FileNameExtention = newFileName,
                        Timestamp = timeStamp
                    };

                    using (var stream = new FileStream(uploadPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        fileData.CopyTo(stream);
                    };

                    await _uow.ExFileReference.Add(fileDetails);
                    await _uow.CompleteAsync();

                    var result = _mapper.Map<ExFileReferenceDto>(fileDetails);

                    return Result.Ok(result);
                }
                else
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Please upload file with correct format!");        
                }
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<List<ExFileReferenceDto>>> UploadMultiFile(List<IFormFile> fileData, string modulId, string moduleName, string fileCategory)
        {
            try
            {
                List<TExFileReference> listData = new List<TExFileReference>();

                foreach (var file in fileData)
                {
                    if (file.Length > 0)
                    {
                        DateTime timeStamp = DateTime.Now;
                        string fileName = Path.GetFileName(file.FileName);
                        string dir = Path.Combine(_uploaderConfig.PathDirectory, moduleName, modulId);
                        Directory.CreateDirectory(dir);
                        Guid mdId = new Guid(modulId);

                        string uploadPath = Path.Combine(dir, fileName);
                        uploadPath = GetNextFilename(uploadPath);

                        string newFileName = Path.GetFileName(uploadPath);

                        var fileDetails = new TExFileReference()
                        {
                            ModulId = mdId,
                            ModulName = moduleName,
                            FileCategory = fileCategory,
                            FileNameExtention = newFileName,
                            Timestamp = timeStamp
                        };

                        using (var stream = new FileStream(uploadPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            file.CopyTo(stream);
                        };

                        await _uow.ExFileReference.Add(fileDetails);
                        await _uow.CompleteAsync();

                        listData.Add(fileDetails);
                    }
                    else
                    {
                        return Result.Fail(ResponseStatusCode.BadRequest + ":Please upload file with correct format!");
                    }
                }

                var result = _mapper.Map<List<ExFileReferenceDto>>(listData);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<List<ExFileReferenceDto>>> GetFileList(ExFileReferenceDto param)
        {
            try
            {
                var repoResult = await _uow.ExFileReference.Set().Where(f => f.ModulName == param.ModulName && f.ModulId == param.ModulId).ToListAsync();

                //if (repoResult.Count == 0)
                //    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<List<ExFileReferenceDto>>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<ExFileReferenceDto>> GetFile(ExFileReferenceDto param)
        {
            try
            {
                var repoResult = await _uow.ExFileReference.Set().FirstOrDefaultAsync(f => f.FileNumber == param.FileNumber);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<ExFileReferenceDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<UploadResult>> ExcelUploader(string appCodeName, string tableName, DateTime uploadDateTime, IFormFile file, R3UserSession userSession, string modulUid, string moduleCode)
        {
            try
            {
                UploadResult uploadResult = null;
                var url = _uploaderConfig.UploadExcel;
                //var url = "https://localhost:44367/api/R3Uploader/UploadExcelBackend";
                var dateString = DateTime.Now.ToString("MMddyyyymmss");
                Guid modulId = new Guid();

                if (!string.IsNullOrEmpty(modulUid))
                {
                    modulId = new Guid(modulUid);
                }

                if (file.Length > 0)
                {
                    string date = uploadDateTime.ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");
                    string fileName = DateTime.Now.ToString("MMddyyyymmss") + "-" + Path.GetFileName(file.FileName);

                    //Copy file to local for get full path
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles", fileName);
                    var stream = new FileStream(filePath, FileMode.Create);
                    file.CopyTo(stream);

                    //Close stream to allow open stream in below 
                    stream.Dispose();

                    var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                    using (var content = new MultipartFormDataContent())
                    {
                        var fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                        content.Add(new StringContent(appCodeName, Encoding.UTF8, "application/json"), "appCodeName");
                        content.Add(new StringContent(tableName, Encoding.UTF8, "application/json"), "tableName");
                        content.Add(new StringContent(userSession.Username, Encoding.UTF8, "application/json"), "uploadBy");
                        content.Add(new StringContent(date, Encoding.UTF8, "application/json"), "uploadDateTime");
                        content.Add(new StreamContent(fileStream), "flDoc", fileName);

                        var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
                        var response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode();

                        var responseString = await response.Content.ReadAsStringAsync();
                        uploadResult = JsonConvert.DeserializeObject<UploadResult>(responseString);

                        //Delete file to minimaze size local folder
                        fileStream.Dispose();
                        File.Delete(filePath);
                    }
                }
                else
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Please upload file with correct format!");
                }

                return Result.Ok(uploadResult);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<ExcelProcessingResult>> ExcelProcessing(string uploadBy, IFormFile file)
        {
            try
            {
                ExcelProcessingResult processingResult = null;
                var url = _uploaderConfig.ExcelProcessing;
                //var url = "https://localhost:44367/api/R3Uploader/ExcelProcessing";

                if (file.Length > 0)
                {
                    string fileName = DateTime.Now.ToString("MMddyyyymmss") + "-" + Path.GetFileName(file.FileName);

                    //Copy file to local for get full path
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles", fileName);
                    var stream = new FileStream(filePath, FileMode.Create);
                    file.CopyTo(stream);

                    //Close stream to allow open stream in below 
                    stream.Dispose();

                    var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                    using (var content = new MultipartFormDataContent())
                    {
                        var fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                        content.Add(new StringContent(uploadBy, Encoding.UTF8, "application/json"), "uploadBy");
                        content.Add(new StreamContent(fileStream), "excelFile", fileName);

                        var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
                        var response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode();

                        var responseString = await response.Content.ReadAsStringAsync();
                        processingResult = JsonConvert.DeserializeObject<ExcelProcessingResult>(responseString);

                        //Delete file to minimaze size local folder
                        fileStream.Dispose();
                        File.Delete(filePath);
                    }
                }
                else
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Please upload file with correct format!");
                }

                return Result.Ok(processingResult);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<string>> DeleteFile(ExFileReferenceDto param)
        {
            try
            {
                if (param != null)
                {
                    var repoResult = await _uow.ExFileReference.Set().FirstOrDefaultAsync(f => f.FileNumber == param.FileNumber);

                    if (repoResult == null)
                        return Result.Fail(ResponseStatusCode.BadRequest + ":File not found!");

                    string pathFile = Path.Combine(_uploaderConfig.PathDirectory, repoResult.ModulName, repoResult.ModulId.ToString(), repoResult.FileNameExtention);

                    _uow.ExFileReference.Remove(repoResult);
                    await _uow.CompleteAsync();

                    if (File.Exists(pathFile))
                    {
                        File.Delete(pathFile);
                    }
                    else
                    {
                        return Result.Fail(ResponseStatusCode.BadRequest + ":File not found!");
                    }

                    return Result.Ok("Success");
                }
                else
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Please select any file!");
                }
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<string>> DeleteMultiFile(List<ExFileReferenceDto> param)
        {
            try
            {
                if (param.Count > 0)
                {
                    foreach (var item in param)
                    {
                        var listData = await _uow.ExFileReference.Set().FirstOrDefaultAsync(f => f.FileNumber == item.FileNumber);

                        if (listData == null)
                        {
                            continue;
                            //return Result.Fail(ResponseStatusCode.BadRequest + ":File not found!");
                        }

                        string pathFile = Path.Combine(_uploaderConfig.PathDirectory, listData.ModulName, listData.ModulId.ToString(), listData.FileNameExtention);

                        _uow.ExFileReference.Remove(listData);
                        await _uow.CompleteAsync();

                        if (File.Exists(pathFile))
                        {
                            File.Delete(pathFile);
                        }
                    }

                    return Result.Ok("Success");
                }
                else
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Please select any file!");
                }
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<UploadResult>> SaveInterfaceToDb(string modulUid, R3UserSession userSession, string moduleCode)
        {
            try
            {
                UploadResult uploadResult = new UploadResult();
                List<ResultMessage> listResult = new List<ResultMessage>();
                ResultMessage message = new ResultMessage();

                //VALIDATION CUSTOM ADA DISINI

                //SAVE DATA DARI INTERFACE KE TABLE ASLI

                return Result.Ok(uploadResult);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
    }
}
