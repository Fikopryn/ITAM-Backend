using System;
using System.Collections.Generic;

namespace Core.Models.Entities.Tables
{
    public partial class TExFileReference
    {
        public Guid FileNumber { get; set; }
        public Guid? ModulId { get; set; }
        public string? ModulName { get; set; }
        public string? FileCategory { get; set; }
        public string? FileNameExtention { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
