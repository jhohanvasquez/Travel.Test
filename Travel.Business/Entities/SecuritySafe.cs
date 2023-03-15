using System;

namespace Travel.Business.Entities
{
    public class SecuritySafe
    {
        public string ServiceCode { get; set; }
        public string AccesoryID { get; set; }
        public int CountryId { get; set; }
        public DateTime SynchronizedDate { get; set; }
        public string OpeningEventCode { get; set; }
        public string ClosingEventCode { get; set; }
    }
}
