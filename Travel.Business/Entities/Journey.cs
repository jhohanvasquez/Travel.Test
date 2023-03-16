using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Travel.Business.Entities
{
    public class Journey
    {
        public decimal IdJourney { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
        public decimal IdTypeRequest { get; set; }
        public List<Flight> Flights { get; set; }
        public bool SuccesSearchs { get; set; }
        public string ErrorMessage { get; set; }

        public class Flight
        {
            public decimal IdFlights { get; set; }
            public string Origin { get; set; }
            public string Destination { get; set; }
            public decimal Price { get; set; }
            public Transport Transport { get; set; }
        }   

        public class Transport
        {
            public string FlightCarrier { get; set; }
            public string FlightNumber { get; set; }
        }


    }
}
