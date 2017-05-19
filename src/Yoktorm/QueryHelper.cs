using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace Yoktorm
{
    public static class QueryHelper
    {
        public static IEnumerable<T> QueryPoco<T>(IDbConnection connection, string statement, object parameter = null, IDbTransaction transaction = null)
        {
            return connection.Query<T>(statement, parameter, transaction);
        }

        public static IEnumerable<dynamic> Query(IDbConnection connection, string statement, object parameter = null, IDbTransaction transaction = null)
        {
            return connection.Query(statement, parameter, transaction);
        }

        public static int Execute(IDbConnection connection, string statement)
        {
            return Execute(connection, statement, null);
        }

        public static int Execute(IDbConnection connection, string statement, object parameter)
        {
            return Execute(connection, statement, parameter, null);
        }

        public static int Execute(IDbConnection connection, string statement, object parameter, IDbTransaction transaction)
        {
            return connection.Execute(statement, parameter, transaction);
        }
    }
}
