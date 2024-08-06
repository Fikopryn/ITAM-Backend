using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.R3Framework.R3port
{
    public class R3portInputDto
    {
        public string reportName { set; get; }
        public string reportOutputType { set; get; } = "xlsx";
        public string reportParam { set; get; }
    }
    public class R3portOutputDto
    {
        public string fileType { set; get; } 
        public byte[] fileData { set; get; } 
        public string fileName { set; get; }
    }
}
