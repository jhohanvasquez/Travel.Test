using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.DataAccess.DTO
{
    public class EventSafeDTO
    {
        public Int32 IdEvent { get; set; }
        public string PhysicalID { get; set; }
        public string TaipId { get; set; }
        public string EventCode { get; set; }
        public string Description { get; set; }
        public DateTime SystemDate { get; set; }
        public long longGPSDate { get; set; }
        public long longSystemDate { get; set; }
        public short waitTime { get; set; }
        public DateTime GpsDate { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string LastUbicationofCommand { get; set; }
    }
}
