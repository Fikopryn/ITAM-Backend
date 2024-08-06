using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InAppSession
{
    public class InAppSessionDto
    {
        public DateTime ExpiredDate { get; set; }
        public string RefreshToken { get; set; }
        public string IpAddress { get; set; }
    }
}
