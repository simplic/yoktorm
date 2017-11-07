using System;
using System.Data;

namespace Yoktorm
{
    public interface IDatabaseProvider
    {
        Type GetClrTypeByName(string typeName);
        Type GetClrType(DbType dbType);
        Type GetClrType(int dbType);
        DbType GetDbType(Type clrType);
        Db.DatabaseStructure GetStructure(IDbConnection connection);
        IDbConnection GetConnection(string connectionString);
        string GetAsParameterName(string name);
    }
}
