using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Travel.Business.Contracts;
using Travel.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Travel.DataAccess.Common
{
    public class TableRepository<T> : ITableRepository<T> where T : TableEntity, new()
    {
        private readonly CloudTable table;

        public TableRepository(string connectionString, string tableName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            table = tableClient.GetTableReference(tableName);
            table.CreateIfNotExistsAsync();           
        }

        public async Task<TableResult> InsertOrMerge(T entity)
        {
            return await table.ExecuteAsync(TableOperation.Insert(entity));
        }
    }   
}
