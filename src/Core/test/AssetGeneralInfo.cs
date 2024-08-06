using System;
using System.Collections.Generic;

namespace Core.test
{
    public partial class AssetGeneralInfo
    {
        public int? CompanyId { get; set; }
        public string SerialNumber { get; set; }
        public string AssetName { get; set; }
        public string AssetFunction { get; set; }
        public string AssetNo { get; set; }
        public string ContractNo { get; set; }
        public string Dono { get; set; }
        public int? ProductCatalogId { get; set; }
        public int? AssetStatusId { get; set; }
        public int? AssetStatusFinId { get; set; }
        public bool? AssetAknowledgement { get; set; }
        public string AdditionalInfo { get; set; }
        public string AreaId { get; set; }
        public string SubAreaId { get; set; }
        public string AssetBuilding { get; set; }
        public string AssetFloorRoom { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? InstallationDate { get; set; }
        public DateTime? LastInventoryDate { get; set; }
        public string UsedBy { get; set; }
        public string OwnedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual AssetStatus AssetStatus { get; set; }
        public virtual AssetStatusFin AssetStatusFin { get; set; }
        public virtual Company Company { get; set; }
        public virtual Contract ContractNoNavigation { get; set; }
        public virtual ProductCatalog ProductCatalog { get; set; }
        public virtual Employee UsedByNavigation { get; set; }
    }
}
