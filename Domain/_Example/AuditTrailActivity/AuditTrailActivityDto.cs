﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain._Example.AuditTrailActivity
{
    public class AuditTrailActivityDto
    {
        public decimal ActivityNumber { get; set; }
        public string? ModulName { get; set; }
        public Guid? ModulId { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Action { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? Remarks { get; set; }
    }
}