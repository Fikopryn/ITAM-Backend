using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ITAM.AdminCon.Company
{
    public class CompanyDto
    {
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyType { get; set; }
        public string Abbreviation { get; set; } = null!;
        public string? Website { get; set; }
        public bool? CompanyStatus { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = null!;
    }
}
