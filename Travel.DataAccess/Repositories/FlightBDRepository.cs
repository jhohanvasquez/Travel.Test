using Dapper;
using Microsoft.Extensions.Options;
using Travel.Business.Contracts;
using Travel.Business.Entities;
using Travel.DataAccess.Config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;

namespace Travel.DataAccess.Repositories
{
    public class FlightBDRepository : IFlightBDRepository
    {
        private readonly DataBaseSettings _settings;
        public FlightBDRepository(IOptions<DataBaseSettings> settings)
        {
            _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        #region Get

        public int GetTypeRequest(string integrateCode)
        {
            string sqlQuery = "SELECT IdTypeRequest FROM [dbo].[TbTypeRequest] WHERE IntegrateCode = @IntegrateCode";
            using (var connection = new SqlConnection(_settings.ConnectionString))
            {
                return (int)connection.Query<int?>(sqlQuery, new { IntegrateCode = integrateCode }).FirstOrDefault();
            }
        }

        public Journey GetJourneySearch(OperationRequest operationRequest)
        {
            string sqlQuery = "SELECT * FROM [dbo].[TbJourney] Journey WHERE Origin = '@Origin' AND Destination = '@Destination'";
            sqlQuery = sqlQuery.Replace("@Origin", operationRequest.Origin);
            sqlQuery = sqlQuery.Replace("@Destination", operationRequest.Destination);
            using var connection = new SqlConnection(_settings.ConnectionString);
            var result = connection.Query<Journey>(sqlQuery, new { Origin = operationRequest.Origin, Destination = operationRequest.Destination }).FirstOrDefault();
            return result;
        }


        public List<Journey.Flight> GetFlightsSearch(decimal idJourney)
        {
            string sqlQuery = "SELECT * FROM [dbo].[TbFlights] Flights WHERE IdJourney = @IdJourney";
            sqlQuery = sqlQuery.Replace("IdJourney", Convert.ToString(idJourney));
            using var connection = new SqlConnection(_settings.ConnectionString);
            var result = connection.Query<Journey.Flight>(sqlQuery, new { IdJourney = idJourney }).ToList();
            return result;
        }

        public Journey.Transport GetTransportSearch(decimal idFlight)
        {
            string sqlQuery = "SELECT * FROM [dbo].[TbTransport] Transport WHERE IdFlight = @IdFlight";
            sqlQuery = sqlQuery.Replace("IdFlight", Convert.ToString(idFlight));
            using var connection = new SqlConnection(_settings.ConnectionString);
            var result = connection.Query<Journey.Transport>(sqlQuery, new { IdJourney = idFlight }).FirstOrDefault();
            return result;
        }

        #endregion

        #region Insert


        public decimal AddJourney(Journey journey)
        {
            string sqlInsertBase = "INSERT INTO TbJourney (Origin, Destination, Price, IdTypeRequest)";
            sqlInsertBase += "VALUES('@Origin', '@Destination', '@Price', '@IdTypeRequest'); SELECT SCOPE_IDENTITY();";

            sqlInsertBase = sqlInsertBase.Replace("@Origin", journey.Origin);
            sqlInsertBase = sqlInsertBase.Replace("@Destination", journey.Destination);
            sqlInsertBase = sqlInsertBase.Replace("@Price", Convert.ToString(journey.Price));
            sqlInsertBase = sqlInsertBase.Replace("@IdTypeRequest", Convert.ToString(journey.IdTypeRequest));

            using (var connection = new SqlConnection(_settings.ConnectionString))
            {
                var result = connection.ExecuteScalar(sqlInsertBase, commandTimeout: 120);
                return (decimal)connection.ExecuteScalar(sqlInsertBase, commandTimeout: 120);
            }
        }

        public decimal AddFlight(Journey.Flight flight, decimal idJourney)
        {

            string sqlInsertBase = "INSERT INTO TbFlights (IdJourney, Origin, Destination, Price)";
            sqlInsertBase += "VALUES('@IdJourney', '@Origin', '@Destination', '@Price'); SELECT SCOPE_IDENTITY();";

            sqlInsertBase = sqlInsertBase.Replace("@IdJourney", Convert.ToString(idJourney));
            sqlInsertBase = sqlInsertBase.Replace("@Origin", flight.Origin);
            sqlInsertBase = sqlInsertBase.Replace("@Destination", flight.Destination);
            sqlInsertBase = sqlInsertBase.Replace("@Price", Convert.ToString(flight.Price));

            using (var connection = new SqlConnection(_settings.ConnectionString))
            {
                return (decimal)connection.ExecuteScalar(sqlInsertBase.ToString(), commandTimeout: 120);
            }
        }

        public void AddTransport(Journey.Transport transport, decimal IdFlights)
        {
            string sqlInsertBase = "INSERT INTO TbTransport (IdFlights, FlightCarrier, FlightNumber)";
            sqlInsertBase += "VALUES('@IdFlights', '@FlightCarrier', '@FlightNumber');";

            sqlInsertBase = sqlInsertBase.Replace("@IdFlights", Convert.ToString(IdFlights));
            sqlInsertBase = sqlInsertBase.Replace("@FlightCarrier", transport.FlightCarrier);
            sqlInsertBase = sqlInsertBase.Replace("@FlightNumber", transport.FlightNumber);

            using (var connection = new SqlConnection(_settings.ConnectionString))
            {
                connection.ExecuteScalar(sqlInsertBase.ToString(), commandTimeout: 120);
            }
        }

        #endregion

    }
}
