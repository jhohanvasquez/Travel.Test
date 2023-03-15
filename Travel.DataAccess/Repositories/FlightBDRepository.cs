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

        public List<SecuritySafeStatus> GetFlightBDSearchs(List<string> serviceCodes)
        {
            DateTime date = new DateTime(DateTime.UtcNow.AddHours(-5).Year, DateTime.UtcNow.AddHours(-5).Month, DateTime.UtcNow.AddHours(-5).Day, 0, 0, 0);
            List<SecuritySafeStatus> securitySafeStatus = new List<SecuritySafeStatus>();
            string sqlQuery = " SELECT	Sa.ServiceCode, Sa.OpeningEventCode, Sa.ClosingEventCode, Ss.Status, Ss.StatusDescription,";
            sqlQuery += " Ss.Date, Ss.UserName, Ss.Address, Ss.Latitude, Ss.Longitude, Ss.CustomerPoint,";
            sqlQuery += " (SELECT COUNT(1) FROM SafeCommands WHERE RowType = 'COMMANDEXECUTED' AND ";
            sqlQuery += " GenerationDate > @Date AND ServiceCode = Sa.ServiceCode) TotalCommands";
            sqlQuery += " FROM[dbo].[Safes] Sa LEFT JOIN[dbo].[SafeStatus] Ss ON Sa.ServiceCode = Ss.ServiceCode WHERE Sa.ServiceCode IN @ServiceCodes ORDER BY ServiceCode";

            using (var connection = new SqlConnection(_settings.ConnectionString))
            {
                securitySafeStatus = connection.Query<SecuritySafeStatus>(sqlQuery.ToString(), new { ServiceCodes = serviceCodes, Date = date.ToString(_settings.FormatDate) }).ToList();
            }

            return securitySafeStatus;
        }


        public void Add(SecuritySafeEvent securitySafeEvent)
        {
            try
            {
                    //List<SecuritySafeCommand> securitySafes = new List<SecuritySafeCommand>();
                
                    //string sqlInsertBase = "INSERT INTO SafeCommands (ServiceCode, GenerationDate, GenerationDateGMT, ";
                    //sqlInsertBase += "Address, State, Town,District,Suburb, Latitude, Longitude, CustomerPointName, CommandType,";
                    //sqlInsertBase += "Description,EventCode, UnifiedEventCode, UserName, RowType)";
                    //sqlInsertBase += "VALUES('@ServiceCode', '@GenerationDate1', '@GenerationDateGMT', '@Address',";
                    //sqlInsertBase += "'@State', '@Town', '@District','@Suburb', @Latitude, @Longitude, '@CustomerPointName',";
                    //sqlInsertBase += "'@CommandType', '@Description', '@EventCode', '@UnifiedEventCode', '@UserName', '@RowType');";

                    //sqlInsertBase = sqlInsertBase.Replace("@ServiceCode", securitySafeEvent.ServiceCode);
                    //sqlInsertBase = sqlInsertBase.Replace("@GenerationDate1", securitySafeEvent.GenerationDate.ToString(_settings.FormatDate));
                    //sqlInsertBase = sqlInsertBase.Replace("@GenerationDateGMT", securitySafeEvent.GenerationDateGMT.ToString(_settings.FormatDate));
                    //sqlInsertBase = sqlInsertBase.Replace("@Address", securitySafeEvent.Address);
                    //sqlInsertBase = sqlInsertBase.Replace("@State", securitySafeEvent.State);
                    //sqlInsertBase = sqlInsertBase.Replace("@Town", securitySafeEvent.Town);
                    //sqlInsertBase = sqlInsertBase.Replace("@District", securitySafeEvent.District);
                    //sqlInsertBase = sqlInsertBase.Replace("@Suburb", securitySafeEvent.Suburb);
                    //sqlInsertBase = sqlInsertBase.Replace("@Latitude", securitySafeEvent.Latitude.ToString().Replace(",", "."));
                    //sqlInsertBase = sqlInsertBase.Replace("@Longitude", securitySafeEvent.Longitude.ToString().Replace(",", "."));
                    //sqlInsertBase = sqlInsertBase.Replace("@CustomerPointName", securitySafeEvent.CustomerPointName);
                    //sqlInsertBase = sqlInsertBase.Replace("@CommandType", securitySafeEvent.CommandType.ToString());
                    //sqlInsertBase = sqlInsertBase.Replace("@Description", securitySafeEvent.Description);
                    //sqlInsertBase = sqlInsertBase.Replace("@EventCode", securitySafeEvent.EventCode);
                    //sqlInsertBase = sqlInsertBase.Replace("@UnifiedEventCode", securitySafeEvent.UnifiedEventCode);
                    //sqlInsertBase = sqlInsertBase.Replace("@UserName", securitySafeEvent.UserName);
                    //sqlInsertBase = sqlInsertBase.Replace("@RowType", securitySafeEvent.RowType.ToString());

                    //using (var connection = new SqlConnection(_settings.ConnectionString))
                    //{
                    //    connection.Execute(sqlInsertBase.ToString(), commandTimeout: 120);
                    //}
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}
