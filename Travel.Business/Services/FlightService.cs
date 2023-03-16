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

        public Journey GetFlight(OperationRequest operationRequest, string routeType, int maxJourneyFlights)
        {
            try
            {
                Journey oJourneyResponse = new Journey();

                CommandResponse commandResponse = GetFlight(routeType);

                if (commandResponse != null && commandResponse.Count > 0)
                {
                    int sumPrice = 0;

                    var originFlights = commandResponse.Where(f => f.departureStation == operationRequest.Origin).Take(maxJourneyFlights).ToList();

                    foreach (var itemOrigin in originFlights)
                    {
                        switch (routeType)
                        {
                            case "0":
                                Journey.Flight oFlightsItem1 = new Journey.Flight();
                                Journey.Flight oFlightsItem2 = new Journey.Flight();

                                var destinationFlights = commandResponse.Where(f => f.departureStation == itemOrigin.arrivalStation && f.arrivalStation == operationRequest.Destination).FirstOrDefault();

                                if (destinationFlights != null)
                                {
                                    oJourneyResponse.Flights = new List<Journey.Flight>();
                                    oFlightsItem1.Origin = itemOrigin.departureStation;
                                    oFlightsItem1.Destination = itemOrigin.arrivalStation;
                                    oFlightsItem1.Price = itemOrigin.price;

                                    oFlightsItem1.Transport = new Journey.Transport();
                                    oFlightsItem1.Transport.FlightCarrier = itemOrigin.flightCarrier;
                                    oFlightsItem1.Transport.FlightNumber = itemOrigin.flightNumber;

                                    oJourneyResponse.Flights.Add(oFlightsItem1);
                                    sumPrice += itemOrigin.price;

                                    oFlightsItem2.Origin = destinationFlights.departureStation;
                                    oFlightsItem2.Destination = destinationFlights.arrivalStation;
                                    oFlightsItem2.Price = destinationFlights.price;

                                    oFlightsItem2.Transport = new Journey.Transport();
                                    oFlightsItem2.Transport.FlightCarrier = destinationFlights.flightCarrier;
                                    oFlightsItem2.Transport.FlightNumber = destinationFlights.flightNumber;

                                    oJourneyResponse.Flights.Add(oFlightsItem2);
                                    sumPrice += destinationFlights.price;
                                }
                                break;
                            case "1":

                                break;
                            case "2":

                                break;
                            default:
                                break;
                        }
                    }

                    oJourneyResponse.IdTypeRequest = _flightBDRepository.GetTypeRequest(routeType);
                    oJourneyResponse.Origin = operationRequest.Origin;
                    oJourneyResponse.Destination = operationRequest.Destination;
                    oJourneyResponse.Price = sumPrice;
                }
                else
                {
                    //result.CodeError = Convert.ToString(commandResponse.StatusCode); 
                    //result.MessageError = commandResponse != null ? commandResponse.Message : "Command not sended";
                }

                if (oJourneyResponse != null)
                {
                    //Guarda en BD si se encuentra
                    SaveTravelSearch(oJourneyResponse);
                }


                return oJourneyResponse;
            }
            catch (Exception ex)
            {
                logRegister.Save(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, "GetFlight", string.Format("{0} {1} {2} {3}", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, ex.Message, ex.StackTrace));
                throw;
            }
        }

        private void SaveTravelSearch(Journey journey)
        {
            try
            {
                var idJourney = _flightBDRepository.AddJourney(journey);
                foreach (var itemFlights in journey.Flights)
                {
                    var idFlight = _flightBDRepository.AddFlight(itemFlights, idJourney);
                    _flightBDRepository.AddTransport(itemFlights.Transport, idFlight);
                }
            }
            catch (Exception ex)
            {
                logRegister.Save(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, "SaveTravelSearch", string.Format("{0} {1} {2} {3}", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, ex.Message, ex.StackTrace));
                throw;
            }

        }

        #endregion

        #region Private Members

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
