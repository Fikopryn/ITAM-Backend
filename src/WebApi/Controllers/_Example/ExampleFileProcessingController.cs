using Core.Helpers;
using Core.Models;
using Domain.Example.FileProcessing;
using Infrastructure.Customs;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace WebApi.Controllers.Example
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleFileProcessingController : CustomControllerBase
    {
        private readonly IFileProcessingService _fileProcSvc;
        private readonly UploaderConfig _uploaderConfig;

        public ExampleFileProcessingController(IFileProcessingService fileProcSvc, IOptions<UploaderConfig> uploaderConfig)
        {
            _fileProcSvc = fileProcSvc;
            _uploaderConfig = uploaderConfig.Value;
        }

        [HttpPost("FileUpload")]
        [SwaggerOperation(Summary = "File Upload")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> FileUpload([FromForm] string moduleId, [FromForm] string moduleName, [FromForm] string fileCategory, IFormFile fileUpload)
        {
            var ret = await _fileProcSvc.UploadFile(fileUpload, moduleId, moduleName, fileCategory);

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp);
            }
        }

        [HttpPost("MultiFileUpload")]
        [SwaggerOperation(Summary = "Multifile Upload")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> MultiFileUpload([FromForm] string moduleId, [FromForm] string moduleName, [FromForm] string fileCategory, List<IFormFile> fileUpload)
        {
            var ret = await _fileProcSvc.UploadMultiFile(fileUpload, moduleId, moduleName, fileCategory);

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp);
            }
        }

        [HttpPost("DownloadFile")]
        [SwaggerOperation(Summary = "Download File By Id")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<FileResult> DownloadFileById(ExFileReferenceDto param)
        {
            var ret = await _fileProcSvc.GetFile(param);

            var uploadPath = Path.Combine(_uploaderConfig.PathDirectory, ret.Value.ModulName, ret.Value.ModulId.ToString(), ret.Value.FileNameExtention);

            byte[] bytes = System.IO.File.ReadAllBytes(uploadPath);

            return File(bytes, "application/octet-stream", ret.Value.FileNameExtention);
        }

        [HttpPost("DownloadMultiFile")]
        [SwaggerOperation(Summary = "Download All File")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<FileResult> DownloadAllFile(ExFileReferenceDto param)
        {
            var ret = await _fileProcSvc.GetFileList(param);

            var zipName = "archiveFile-" + DateTime.Now.ToString("MMddyyyymmss") + ".zip";

            using (MemoryStream ms = new MemoryStream())
            {
                using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    foreach (var item in ret.Value)
                    {
                        string fPath = Path.Combine(_uploaderConfig.PathDirectory, param.ModulName, param.ModulId.ToString(), item.FileNameExtention);
                        var entry = archive.CreateEntry(Path.GetFileName(fPath), CompressionLevel.Fastest);
                        using (var zipStream = entry.Open())
                        {
                            var bytes = System.IO.File.ReadAllBytes(fPath);
                            zipStream.Write(bytes, 0, bytes.Length);
                        }
                    }
                }

                return File(ms.ToArray(), "application/zip", zipName);
            }
        }

        [HttpPost("UploaderExcel")]
        [SwaggerOperation(Summary = "Uploader Excel")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> UploaderExcel([FromForm] string? modulUid, string moduleCode, IFormFile file)
        {
            string tableName = "";

            var ret = await _fileProcSvc.ExcelUploader("PreciseMT", tableName, DateTime.Now, file, UserAction, modulUid, moduleCode) ;

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp);
            }
        }

        [HttpPost("InterfaceToDB")]
        [SwaggerOperation(Summary = "Saving Data From Interface To DB")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> InterfaceToDB([FromBody] InterfaceToDbDto interfaceToDbDto)
        {
            var ret = await _fileProcSvc.SaveInterfaceToDb(interfaceToDbDto.ModuleUid, UserAction, interfaceToDbDto.ModuleCode);

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp);
            }
        }

        [HttpPost("DownloadTemplate")]
        [SwaggerOperation(Summary = "Download Template Upload")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<FileResult> DownloadTemplateUpload([FromForm] string modulCode)
        {
            string flname = DateTime.Now.ToString("MMddyyyy_mmss");
            string fileName = "";
            var path = _uploaderConfig.PathTemplate + "\\" + fileName;

            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", fileName);
        }

        [HttpPost("ExcelProcessing")]
        [SwaggerOperation(Summary = "Excel Processing")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> ExcelProcessing(string uploadBy, IFormFile file)
        {
            var ret = await _fileProcSvc.ExcelProcessing(uploadBy, file);

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp);
            }
        }

        [HttpPost("DeleteFile")]
        [SwaggerOperation(Summary = "Delete File By File ID")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> DeleteFileById(ExFileReferenceDto param)
        {
            var ret = await _fileProcSvc.DeleteFile(param);

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp);
            }
        }

        [HttpPost("DeleteMultiFile")]
        [SwaggerOperation(Summary = "Delete Multi File By File ID")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> DeleteMultiFileById(List<ExFileReferenceDto> param)
        {
            var ret = await _fileProcSvc.DeleteMultiFile(param);

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp);
            }
        }
    }
}
