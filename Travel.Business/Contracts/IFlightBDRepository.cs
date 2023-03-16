using Travel.Business.Entities;
using System;
using System.Collections.Generic;

namespace Travel.Business.Contracts
{
    public interface IFlightBDRepository
    {
        decimal AddJourney(Journey journey);
        decimal AddFlight(Journey.Flight flight, decimal idJourney);
        void AddTransport(Journey.Transport transport, decimal IdFlights);
        int GetTypeRequest(string integrateCode);
        Journey GetJourneySearch(OperationRequest operationRequest);
    }
}