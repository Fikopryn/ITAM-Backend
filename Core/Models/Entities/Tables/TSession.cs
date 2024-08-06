namespace Core.Models.Entities.Tables
{
    public partial class TSession
    {
        public string IpAddress { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime ExpiredDate { get; set; }
    }
}
