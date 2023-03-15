using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Business.Contracts
{
    public interface IDbManager<T> where T : class
    {
        Task<IEnumerable<T>> GetAllQueryString(string query, object parameters = null);

        Task<int> ExecuteStoreProcedure(string spName, DynamicParameters parameters);

        Task<IEnumerable<Object>> ExecuteReaderStoreProcedure(string spName, DataTable dataTable);

        Task<IEnumerable<Object>> ExecuteReaderStoreProcedure(string spName, object dataTable);

        Task<IEnumerable<Object>> ExecuteReaderStoreProcedure(string spName, DynamicParameters parameters);
    }
}
