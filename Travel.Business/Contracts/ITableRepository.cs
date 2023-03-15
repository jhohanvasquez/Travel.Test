using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Business.Contracts
{
    public interface ITableRepository<T> where T : TableEntity
    {
        Task<TableResult> InsertOrMerge(T entity);
    }
}
