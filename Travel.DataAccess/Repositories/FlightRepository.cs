using Travel.Business.Contracts;
using Travel.Business.Entities;
using System;
using System.Collections.Generic;

namespace Travel.DataAccess.Repositories
{
    public class FlightRepository : IFlightRepository
    {

        private readonly IRestClient<CommandResponse> restCommandClient;
        private readonly ILogRegister logRegister;

        public FlightRepository(IRestClient<CommandResponse> restCommandClient, ILogRegister logRegister)
        {
            this.restCommandClient = restCommandClient;
            this.logRegister = logRegister;
        }

        public CommandResponse GetFlight(string routeType)
        {
            try
            {
                var result = this.restCommandClient.GetAsync(null, routeType);
                return result;
            }
            catch (Exception ex)
            {
                logRegister.Save(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, ex.Message);
                throw;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    logRegister.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {

            Dispose(true);

            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
