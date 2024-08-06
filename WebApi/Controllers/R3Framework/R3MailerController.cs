using Core.Helpers;
using Core.Models;
using Domain.R3Framework.R3Mailer;
using Infrastructure.Customs;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers.R3Framework
{
    [Route("api/r3/mailer")]
    [ApiController]
    public class R3MailerController : CustomControllerBase
    {
        private readonly R3tinaConfig _r3Config;
        private readonly WorkflowConfig _wfConfig;
        private readonly MailerConfig _mailConfig;
        private readonly IR3Mailer _r3MailSvc;

        public R3MailerController(
            IOptions<R3tinaConfig> r3Config,
            IOptions<WorkflowConfig> wfConfig,
            IOptions<MailerConfig> mailConfig,
            IR3Mailer r3mailSvc)
        {
            _r3Config = r3Config.Value;
            _wfConfig = wfConfig.Value;
            _mailConfig = mailConfig.Value;
            _r3MailSvc = r3mailSvc;
        }

        [HttpPost("sendemail")]
        [SwaggerOperation(Summary = "Send Email")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> SendEmail(string emailTo)
        {
            var emailPayload = new MailerPayload
            {
                From = _mailConfig.FromEmail,
                To = emailTo,
                Cc = _mailConfig.CcEmail,
                Bcc = _mailConfig.BccEmail,
                AppName = _r3Config.AppName,
                Subject = _wfConfig.EmailNotificationSubject,
                TemplateName = _wfConfig.EmailNotificationTemplateName,
                AppSupportMail = _mailConfig.ApplicationSupportEmail,
                ContentModelJson = new ContentModelJson()
                {
                    Name = "Test R3 Workflow"
                }
            };

            var ret = await _r3MailSvc.SendEmail(UserAction, emailPayload);

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
