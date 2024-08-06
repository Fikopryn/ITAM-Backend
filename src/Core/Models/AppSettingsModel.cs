namespace Core.Models
{
    public class AppSettingsConfig
    {
        public string ApplicationUrl { get; set; }
        public string CorsUrl { get; set; }
        public string HangfireUser { get; set; }
        public string HangfirePass { get; set; }
        public string AppName { get; set; }
        public string AppEnv { get; set; }
    }

    public class R3OAuthConfig
    {
        public string RealmName { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AuthServerUrl { get; set; }
        public double ExpiredInDays { get; set; }
    }

    public class R3tinaConfig
    {
        public string AppName { get; set; }
        public string R3OAuth { get; set; }
        public string R3DataManagement { get; set; }
        public string R3Mailer { get; set; }
        public string R3Workflow { get; set; }
        public string R3tinaApi { get; set; }
    }

    public class WorkflowConfig
    {
        public string ProcessId { get; set; }
        public string OrgId { get; set; }
        public string EmailNotificationSubject { get; set; }
        public string EmailNotificationTemplateName { get; set; }
    }

    public class MailerConfig
    {
        public string ApplicationSupportEmail { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string CcEmail { get; set; }
        public string BccEmail { get; set; }
    }

    public class UploaderConfig
    {
        public string UploadExcel { get; set; }
        public string ExcelProcessing { get; set; }
        public string PathDirectory { get; set; }
        public string PathTemplate { get; set; }
    }

    public class R3portConfig
    {
        public string ReportUrl { get; set; }
    }
}
