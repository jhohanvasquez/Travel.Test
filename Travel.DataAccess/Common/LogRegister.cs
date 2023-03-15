using AutoMapper;
using Microsoft.Extensions.Options;
using Travel.Business.Configuration;
using Travel.Business.Contracts;
using Travel.Business.Entities;
using Travel.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.DataAccess.Common
{
    public class LogRegister : ILogRegister
    {
        private readonly StorageSettings storageSettings;
        private readonly ITableRepository<DataLogDTO> dataLog;

        public LogRegister(ITableRepository<DataLogDTO> dataLog, IOptions<ApiSettings> storageSettings)
        {
            this.storageSettings = storageSettings.Value.environmentVariables.StorageSettings;
            this.dataLog = dataLog;
        }

        public async void Save(string classMessage, string method, string message)
        {
            string messageSave = string.Format("{0} {1} {2} {3}", DateTime.Now.ToString(), classMessage, method, message);

            Console.WriteLine(messageSave);

            DataLogDTO dataLogDTO = MapDataLogError(message);

            Mapper.Map<DataLogDTO>(dataLogDTO);

            await this.dataLog.InsertOrMerge(dataLogDTO);
        }
       
        private DataLogDTO MapDataLogError(string message)
        {
            DataLogDTO dataLogDTO = new DataLogDTO();

            dataLogDTO.PartitionKey = Guid.NewGuid().ToString();
            dataLogDTO.RowKey = "Error";                       
            dataLogDTO.ErrorMessage = message;
            dataLogDTO.DateLog = DateTime.UtcNow.AddHours(-5);

            return dataLogDTO;
        }

        #region IDisposable Support

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
