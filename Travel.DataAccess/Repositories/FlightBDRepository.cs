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
            string sqlQuery = "SELECT * FROM [dbo].[TbJourney] WHERE Origin = '@Origin' AND Destination = '@Destination'";
            using var connection = new SqlConnection(_settings.ConnectionString);
            return connection.Query<Journey>(sqlQuery, new { Origin = operationRequest.Origin, Destination = operationRequest.Destination }).FirstOrDefault();
        }

        public decimal AddJourney(Journey journey)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return 0;
            }
        }

        public decimal AddFlight(Journey.Flight flight, decimal idJourney)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return 0;
            }
        }

        public void AddTransport(Journey.Transport transport, decimal IdFlights)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}
