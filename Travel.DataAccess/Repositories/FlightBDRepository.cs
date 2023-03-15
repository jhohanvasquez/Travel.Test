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

        public int AddJourney(Journey journey)
        {
            try
            {
                string sqlInsertBase = "INSERT INTO SafeCommands (Origin, Destination, Price, 'IdTypeRequest')";
                sqlInsertBase += "VALUES('@Origin', '@Destination', '@Price', '@IdTypeRequest');";

                sqlInsertBase = sqlInsertBase.Replace("@Origin", journey.Origin);
                sqlInsertBase = sqlInsertBase.Replace("@Destination", journey.Destination);
                sqlInsertBase = sqlInsertBase.Replace("@Price", Convert.ToString(journey.Price));
                sqlInsertBase = sqlInsertBase.Replace("@IdTypeRequest", Convert.ToString(journey.IdTypeRequest));

                using (var connection = new SqlConnection(_settings.ConnectionString))
                {
                    return (int)connection.ExecuteScalar(sqlInsertBase.ToString(), commandTimeout: 120);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return 0;
            }
        }

        public int AddFlight(Journey.Flight flight, int idJourney)
        {
            try
            {
                string sqlInsertBase = "INSERT INTO SafeCommands (IdJourney, Origin, Destination, 'IdTypeRequest')";
                sqlInsertBase += "VALUES('@IdJourney', '@Origin', '@Destination', '@IdTypeRequest');";

                sqlInsertBase = sqlInsertBase.Replace("@IdJourney", Convert.ToString(idJourney));
                sqlInsertBase = sqlInsertBase.Replace("@Origin", flight.Origin);
                sqlInsertBase = sqlInsertBase.Replace("@Destination", flight.Destination);
                sqlInsertBase = sqlInsertBase.Replace("@Price", Convert.ToString(flight.Price));

                using (var connection = new SqlConnection(_settings.ConnectionString))
                {
                    return (int)connection.ExecuteScalar(sqlInsertBase.ToString(), commandTimeout: 120);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return 0;
            }
        }

        public void AddTransport(Journey.Transport transport, int IdFlights)
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
