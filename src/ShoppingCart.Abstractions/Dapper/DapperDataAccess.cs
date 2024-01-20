using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShoppingCart.Abstractions.Dapper.Interfaces;
using System.Data;

namespace ShoppingCart.Abstractions.Dapper
{
    public class DapperDataAccess : IDapperDataAccess
    {
        private readonly string? connString;
        private readonly ILogger<DapperDataAccess> logger;

        /// <summary>
        /// Constructor Dapper Data Access
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        public DapperDataAccess(IConfiguration configuration, ILogger<DapperDataAccess> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            connString = configuration != null ? configuration.GetConnectionString("SqlConnection") : throw new ArgumentNullException(nameof(configuration)); ;
        }

        /// <summary>
        /// Create db connection for Dapper
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(connString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Execute query for Dapper
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Execute(string sqlQuery, object? param = null)
        {
            using var connection = CreateConnection();
            try
            {
                var result = connection.Execute(sqlQuery, param);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return 0;
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        /// <summary>
        /// Execute query for Dapper
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int ExecuteScalar(string sqlQuery, object? param = null)
        {
            using var connection = CreateConnection();
            try
            {
                var result = connection.ExecuteScalar(sqlQuery, param);
                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return 0;
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        /// <summary>
        /// Execute query for Dapper
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> Query<TEntity>(string sqlQuery, object? param = null)
        {
            using var connection = CreateConnection();
            try
            {
                var result = connection.Query<TEntity>(sqlQuery, param);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new List<TEntity>();
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        /// <summary>
        /// First or Default Response for Dapper query
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public TEntity? QueryFirstOrDefault<TEntity>(string sqlQuery, object? param = null)
        {
            using var connection = CreateConnection();
            try
            {
                var result = connection.QueryFirstOrDefault<TEntity>(sqlQuery, param);
                
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return default;
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        /// <summary>
        /// Single or Default Response for Dapper query
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public TEntity? QuerySingleOrDefault<TEntity>(string sqlQuery, object? param = null)
        {
            using var connection = CreateConnection();

            try
            {
                var result = connection.QuerySingleOrDefault<TEntity>(sqlQuery, param);
                
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return default;
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }
    }
}
