using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoktorm.Db;

namespace Yoktorm.SQLite
{
    public class SQLiteProvider : IDatabaseProvider
    {
        public IDbConnection Get(string connectionString)
        {
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            return connection;
        }

        public Type GetClrType(int dbType)
        {
            throw new NotImplementedException();
        }

        public Type GetClrType(DbType dbType)
        {
            throw new NotImplementedException();
        }

        public Type GetClrType(string typeName)
        {
            throw new NotImplementedException();
        }

        public DbType GetDbType(Type clrType)
        {
            throw new NotImplementedException();
        }

        public DatabaseStructure GetStructure(IDbConnection dbConnection)
        {
            throw new NotImplementedException();
        }
    }
}