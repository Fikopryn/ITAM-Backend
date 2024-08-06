using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ITAM.ConManCon.Contract
{
    public class ContractDto
    {
        public int CompanyId { get; set; }
        public string ContractOwner { get; set; } = null!;
        public string ContractNo { get; set; } = null!;
        public string Subject { get; set; }
        public int SupplierId { get; set; }
        public DateTime? StartContract { get; set; }
        public DateTime? EndContract { get; set; }
        public decimal? ContractValue { get; set; }
        public byte[] ContractDoc { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = null!;
    }
}
