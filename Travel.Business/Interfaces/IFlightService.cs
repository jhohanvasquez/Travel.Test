using Travel.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Travel.Business.Interfaces
{
    public interface IFlightService : IDisposable
    {
        Journey GetFlight(OperationRequest operationRequest, string routeType, int maxJourneyFlights);
    }
}

