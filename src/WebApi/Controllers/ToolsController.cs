using Core.Extensions;
using Core.Helpers;
using Infrastructure.Customs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolsController : CustomControllerBase
    {

        public ToolsController()
        {
        }

        [HttpPost("Encrypt")]
        [SwaggerOperation(Summary = "String Encryption")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public IActionResult EncryptPassword(string stringToEncrypt)
        {
            try
            {
                return Ok(EncryptionHelper.AesEncrypt(stringToEncrypt));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.GetMessage());
            }
        }

        [HttpPost("Decrypt")]
        [SwaggerOperation(Summary = "String Decryption")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public IActionResult DecryptPassword(string stringToDecrypt)
        {
            try
            {
                return Ok(EncryptionHelper.AesDecrypt(stringToDecrypt));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.GetMessage());
            }
        }
    }
}
