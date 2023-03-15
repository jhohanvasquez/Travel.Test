using Travel.Business.Entities;
using System;
using System.Collections.Generic;

namespace Travel.Business.Contracts
{
    public interface IFlightBDRepository
    {
        int AddJourney(Journey journey);
    }
}