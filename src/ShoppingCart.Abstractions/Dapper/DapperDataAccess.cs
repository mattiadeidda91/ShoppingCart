using Dapper;
using Microsoft.Extensions.Logging;
using ShoppingCart.Abstractions.Dapper.DbContext;
using ShoppingCart.Abstractions.Dapper.Interfaces;

namespace ShoppingCart.Abstractions.Dapper
{
    public class DapperDataAccess : IDapperDataAccess
    {
        private readonly ILogger<DapperDataAccess> logger;
        private readonly ShoppingCartContext dbContext;

        /// <summary>
        /// Constructor Dapper Data Access
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        public DapperDataAccess(ShoppingCartContext dbContext, ILogger<DapperDataAccess> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext;
        }

        #region Sync

        /// <summary>
        /// Execute query for Dapper
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Execute(string sqlQuery, object? param = null)
        {
            using var connection = dbContext.CreateConnection();
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
        /// ExecuteScalar query for Dapper
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int ExecuteScalar(string sqlQuery, object? param = null)
        {
            using var connection = dbContext.CreateConnection();
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
        /// Query for Dapper
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> Query<TEntity>(string sqlQuery, object? param = null)
        {
            using var connection = dbContext.CreateConnection();
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
            using var connection = dbContext.CreateConnection();
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
            using var connection = dbContext.CreateConnection();

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

        #endregion

        #region Async

        /// <summary>
        /// ExecuteAsync query for Dapper
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string sqlQuery, object? param = null)
        {
            using var connection = dbContext.CreateConnection();
            try
            {
                var result = await connection.ExecuteAsync(sqlQuery, param);
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
        /// ExecuteScalarAsync query for Dapper
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<int> ExecuteScalarAsync(string sqlQuery, object? param = null)
        {
            using var connection = dbContext.CreateConnection();
            try
            {
                var result = await connection.ExecuteScalarAsync(sqlQuery, param);
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
        /// QueryAsync query for Dapper
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string sqlQuery, object? param = null)
        {
            using var connection = dbContext.CreateConnection();
            try
            {
                var result = await connection.QueryAsync<TEntity>(sqlQuery, param);
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
        /// QueryFirstOrDefaultAsync for Dapper query
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<TEntity?> QueryFirstOrDefaultAsync<TEntity>(string sqlQuery, object? param = null)
        {
            using var connection = dbContext.CreateConnection();
            try
            {
                var result = await connection.QueryFirstOrDefaultAsync<TEntity>(sqlQuery, param);

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
        /// QuerySingleOrDefaultAsync for Dapper query
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<TEntity?> QuerySingleOrDefaultAsync<TEntity>(string sqlQuery, object? param = null)
        {
            using var connection = dbContext.CreateConnection();

            try
            {
                var result = await connection.QuerySingleOrDefaultAsync<TEntity>(sqlQuery, param);

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
        /// QueryMultipleAsync for Dapper query
        /// </summary>
        /// <typeparam name="TEntity1"></typeparam>
        /// <typeparam name="TEntity2"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<TEntity1?>, IEnumerable<TEntity2?>)> QueryMultipleAsync<TEntity1, TEntity2>(string sqlQuery, object? param = null)
        {
            using var connection = dbContext.CreateConnection();

            try
            {
                var result = await connection.QueryMultipleAsync(sqlQuery, param);

                var entities1 = result.Read<TEntity1>();
                var entities2 = result.Read<TEntity2>();

                return (entities1, entities2);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return (default, default);
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        #endregion
    }
}
