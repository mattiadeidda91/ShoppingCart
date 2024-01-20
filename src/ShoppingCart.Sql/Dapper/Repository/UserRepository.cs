using ShoppingCart.Abstractions.Dapper.Entities;
using ShoppingCart.Abstractions.Dapper.Interfaces;
using static ShoppingCart.Abstractions.Dapper.DataAccessGlobalConst;

namespace ShoppingCart.Sql.Dapper.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IDapperDataAccess dapperDataAccess)
            : base(TableNameUsers, dapperDataAccess)
        { }

        public bool Exists(string lastname)
        {
            return dapperDataAccess.QuerySingleOrDefault<User>(
                $"SELECT * FROM {TableNameUsers} WHERE Lastname=@Lastname", 
                new { Lastname = lastname }
            ) != null;
        }
    }
}
