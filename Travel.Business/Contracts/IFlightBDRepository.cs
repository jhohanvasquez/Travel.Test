using Travel.Business.Entities;
using System;
using System.Collections.Generic;

namespace Travel.Business.Contracts
{
    public interface IFlightBDRepository
    {
        void Add(SecuritySafeEvent securitySafeEvent);
        List<SecuritySafeStatus> GetFlightBDSearchs(List<string> serviceCodes);
    }
}