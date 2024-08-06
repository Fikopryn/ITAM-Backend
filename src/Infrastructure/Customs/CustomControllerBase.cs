using Domain.R3Framework.R3User;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Customs
{
    public class CustomControllerBase : ControllerBase
    {
        public string HeaderAuthorization { get; set; }
        public R3UserSession UserAction { get; set; }
        public string IpAddress { get; set; }
    }
}
