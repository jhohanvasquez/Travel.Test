using Dapper;
using Travel.Business.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Travel.DataAccess.Common
{
    public class DbManager<T> : IDbManager<T> where T : class
    {
        private readonly string connectionString;

        public DbManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<T>> GetAllQueryString(string query, object parameters = null)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {                
                return await connection.QueryAsync<T>(query, parameters);
            }
        }

        public async Task<int> ExecuteStoreProcedure(string spName, DynamicParameters parameters)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {                
              return await connection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
            }
        }



        public async Task<IEnumerable<Object>> ExecuteReaderStoreProcedure(string spName, DataTable dataTable)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {                
                var result = await connection.QueryAsync(spName, new { Placas = dataTable }, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<IEnumerable<Object>> ExecuteReaderStoreProcedure(string spName, object objectParam)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {                
                var result = await connection.QueryAsync(spName, objectParam, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<IEnumerable<Object>> ExecuteReaderStoreProcedure(string spName, DynamicParameters parameters)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {                
                var result = await connection.QueryAsync(spName, parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
}
