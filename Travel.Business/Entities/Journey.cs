using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Travel.Business.Entities
{
    public class Journey
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        public int IdTypeRequest { get; set; }
        public List<Flight> Flights { get; set; }

        public class Flight
        {
            public string Origin { get; set; }
            public string Destination { get; set; }
            public double Price { get; set; }
            public Transport Transport { get; set; }
        }   

        public class Transport
        {
            public string FlightCarrier { get; set; }
            public string FlightNumber { get; set; }
        }


    }
}
