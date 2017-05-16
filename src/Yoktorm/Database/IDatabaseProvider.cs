using System;
using System.Data;

namespace Yoktorm
{
    public interface IDatabaseProvider
    {
        Type GetClrType(string typeName);
        Type GetClrType(DbType dbType);
        Type GetClrType(int dbType);
        DbType GetDbType(Type clrType);
        Db.DatabaseStructure GetStructure(IDbConnection dbConnection);
        IDbConnection Get(string connectionString);
    }
}
