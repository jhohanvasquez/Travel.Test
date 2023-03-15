using Travel.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Business.Contracts
{
    public interface IFlightRepository : IDisposable
    {
        CommandResponse GetFlight(string routeType);
      
    }
}
