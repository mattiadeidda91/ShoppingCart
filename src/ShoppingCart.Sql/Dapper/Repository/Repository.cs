using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Dapper.Interfaces;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace ShoppingCart.Sql.Dapper.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly string tableName;
        protected readonly IDapperDataAccess dapperDataAccess;

        /// <summary>
        /// Constructor Dapper Repository
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dapperDataAccess"></param>
        protected Repository(string tableName, IDapperDataAccess dapperDataAccess)
        {
            this.dapperDataAccess = dapperDataAccess;
            this.tableName = tableName;
        }

        #region Sync

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll()
        {
            return dapperDataAccess.Query<TEntity>($"SELECT * FROM {tableName}");
        }

        /// <summary>
        /// Get all whit where contidion
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll(Dictionary<string, string> criteria)
        {
            var query = new StringBuilder($"SELECT * FROM {tableName} WHERE ");

            if (criteria != null)
                foreach (var key in criteria.Keys)
                {
                    query.Append(CultureInfo.InvariantCulture, $"{key}={criteria[key]} ");
                }

            return dapperDataAccess.Query<TEntity>(query.ToString());
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="guidEntity"></param>
        public int Delete(int id)
        {
            return dapperDataAccess.Execute($"DELETE FROM {tableName} WHERE Id=@Id", new { Id = id });
        }

        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public TEntity? GetById(int id)
        {
            return dapperDataAccess.QuerySingleOrDefault<TEntity>($"SELECT * FROM {tableName} WHERE Id =@Id", new { Id = id });
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(TEntity entity)
        {
            var updateQuery = GenerateUpdateQuery();

            return dapperDataAccess.Execute(updateQuery, entity);
        }

        ///<summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(TEntity entity)
        {
            var insertQuery = GenerateInsertQuery();

            var entityId = dapperDataAccess.ExecuteScalar(insertQuery, entity);

            return entityId;
        }


        #endregion

        #region Async

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dapperDataAccess.QueryAsync<TEntity>($"SELECT * FROM {tableName}");
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Dictionary<string, string> criteria)
        {
            var query = new StringBuilder($"SELECT * FROM {tableName} WHERE ");

            if (criteria != null)
                foreach (var key in criteria.Keys)
                {
                    query.Append(CultureInfo.InvariantCulture, $"{key}={criteria[key]} ");
                }

            return await dapperDataAccess.QueryAsync<TEntity>(query.ToString());
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await dapperDataAccess.QueryFirstOrDefaultAsync<TEntity>($"SELECT * FROM {tableName} WHERE Id=@Id", new { Id = id });
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            var updateQuery = GenerateUpdateQuery();

            return await dapperDataAccess.ExecuteAsync(updateQuery, entity);
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            var insertQuery = GenerateInsertQuery();

            return await dapperDataAccess.ExecuteScalarAsync(insertQuery, entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await dapperDataAccess.ExecuteAsync($"DELETE FROM {tableName} WHERE Id=@Id", new { Id = id });
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generate Insert Query
        /// </summary>
        /// <returns></returns>
        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {tableName} ");

            _ = insertQuery.Append('(');

            var properties = GenerateListOfProperties(GetProperties);
            insertQuery.Append(string.Join(", ", properties.Select(prop => prop)));

            _ = insertQuery.Append(") VALUES (");

            insertQuery.Append(string.Join(", ", properties.Select(prop => $"@{prop}")));

            _ = insertQuery
                .Append(");")
                .Append("SELECT CAST(SCOPE_IDENTITY() as int);");

            return insertQuery.ToString();
        }

        /// <summary>
        /// Generate Update Query
        /// </summary>
        /// <returns></returns>
        private string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE {tableName} SET ");
            var properties = GenerateListOfProperties(GetProperties);

            properties.ForEach(property =>
            {
                if (property != null && !property.Equals("Id", StringComparison.Ordinal))
                {
                    updateQuery.Append(CultureInfo.InvariantCulture, $"{property}=@{property},");
                }
            });

            _ = updateQuery.Remove(updateQuery.Length - 1, 1);
            _ = updateQuery.Append(" WHERE Id=@Id");

            return updateQuery.ToString();
        }

        /// <summary>
        /// Generate List Of Properties
        /// </summary>
        /// <param name="listOfProperties"></param>
        /// <returns></returns>
        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
        }

        /// <summary>
        /// Get Properties
        /// </summary>
        private static IEnumerable<PropertyInfo> GetProperties
            => typeof(TEntity).GetProperties();

        #endregion
    }
}
