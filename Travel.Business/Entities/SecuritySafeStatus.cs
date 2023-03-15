using System;

namespace Travel.Business.Entities
{
    public class SecuritySafeStatus
    {
        public string ServiceCode { get; set; }
        public bool Status { get; set; }
        public string StatusDescription { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string CustomerPoint { get; set; }
        public int TotalCommands { get; set; }
    }
}