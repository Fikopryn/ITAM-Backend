using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain._Example.MasterLov
{
    public class MasterLovDto
    {
        public decimal LovId { get; set; }
        public string? LovName { get; set; }
        public string? LovDescriptions { get; set; }
        public string? LovCategory { get; set; }
        public string? LovKey { get; set; }
        public string? LovValue { get; set; }
        public string? LovParentKey { get; set; }
        public decimal? LovSequence { get; set; }
        public string? LovAttr1 { get; set; }
        public string? LovAttr2 { get; set; }
        public string? LovAttr3 { get; set; }
        public bool? IsActive { get; set; }
    }
}
