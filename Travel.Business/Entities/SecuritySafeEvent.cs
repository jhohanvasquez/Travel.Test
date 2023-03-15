
using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Business.Entities
{
    public class SecuritySafeEvent
    {
        public string ServiceCode { get; set; }
        public string Address { get; set; }
        public DateTime GenerationDate { get; set; }
        public DateTime GenerationDateGMT { get; set; }
        public DateTime RecordDate { get; set; }
        public string CustomerPointName { get; set; }
        public string Description { get; set; }
        public string District { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Town { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string EventCode { get; set; }
        public string UnifiedEventCode { get; set; }
        public string UserName { get; set; }
        public string OpeningEventCode { get; set; }
        public string ClosingEventCode { get; set; }
    }
}
