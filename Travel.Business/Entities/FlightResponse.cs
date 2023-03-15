using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Business.Entities
{
    public class FlightResponse
    {
        public string departureStation { get; set; }
        public string arrivalStation { get; set; }
        public string flightCarrier { get; set; }
        public string flightNumber { get; set; }
        public int price { get; set; }
    }
}
