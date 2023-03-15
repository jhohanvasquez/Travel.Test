using Travel.Business.Configuration;
using Travel.Business.Contracts;
using Travel.Business.Entities;
using Travel.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Travel.Business.Services
{
    public class FlightService : IFlightService
    {

        private readonly IFlightRepository _flightRepository;
        private readonly ILogRegister logRegister;
        private readonly IFlightBDRepository _flightBDRepository;

        bool disposed = false;

        public FlightService(IFlightRepository flightRepository, ILogRegister logRegister, IFlightBDRepository flightBDRepository)
        {
            this.logRegister = logRegister;
            _flightRepository = flightRepository;
            _flightBDRepository = flightBDRepository;
        }

        #region Public Members

        public OperationResult GetFlight(OperationRequest operationRequest, string routeType,  int maxJourneyFlights)
        {
            try
            {
                OperationResult result = new OperationResult();

                CommandResponse commandResponse = GetFlight(routeType);

                if (commandResponse != null)
                {
                    //Realizar calculos y guarda en BD si se encuentra
                   // SaveCommandSended(commandVehicle, securitySafe);
                }
                else
                {
                    //result.CodeError = Convert.ToString(commandResponse.StatusCode); 
                    //result.MessageError = commandResponse != null ? commandResponse.Message : "Command not sended";
                }

                return result;
            }
            catch (Exception ex)
            {
                logRegister.Save(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, "GetFlight", string.Format("{0} {1} {2} {3}", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, ex.Message, ex.StackTrace));
                throw;
            }
        }

        //private void SaveTravelSearch(Travel commandVehicle, SecuritySafe securitySafe)
        //{
        //    Travel travelSearch = new Travel();       

        //    _flightBDRepository.Add(securitySafeEvent);
        //}

        #endregion

        #region Private Members

        /// <summary>
        /// Validar busquedas
        /// </summary>
        /// <param name="physicalID"></param>
        /// <returns></returns>
        private SecuritySafeStatus GetSecuritySafe(string physicalID)
        {
            return _flightBDRepository.GetFlightBDSearchs(new List<string>() { physicalID }).FirstOrDefault();
        }

        private CommandResponse GetFlight(string routeType)
        {
            return this._flightRepository.GetFlight(routeType);
        }

        #endregion


        #region IDisposable Support
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
            }

            disposed = true;
        }
        #endregion

    }
}
