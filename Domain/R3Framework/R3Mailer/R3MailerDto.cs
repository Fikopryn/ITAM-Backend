namespace Domain.R3Framework.R3Mailer
{
    public class MailerPayload
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string AppName { get; set; }
        public string Subject { get; set; }
        public string TemplateName { get; set; }
        public string AppSupportMail { get; set; }
        public ContentModelJson ContentModelJson { get; set; }
    }

    public class MailerResponse
    {
        public int resultInt { get; set; }
        public string errMsg { get; set; }
    }

    #region Custom Dto Just For This App
    // Change this property class as r3tina email template needs
    public class ContentModelJson
    {
        public string Name { get; set; }
    }
    #endregion
}
