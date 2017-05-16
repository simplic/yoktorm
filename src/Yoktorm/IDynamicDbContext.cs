using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoktorm
{
    /// <summary>
    /// Contains parts of the db context that does not require any knowledge about the database structure
    /// and will be used in the provider
    /// </summary>
    public interface IDynamicDbContext
    {
        IEnumerable<dynamic> Query(string statement, object parameter = null, IDbTransaction transaction = null);

        IEnumerable<T> QueryPoco<T>(string statement, object parameter = null, IDbTransaction transaction = null);

        int Execute(string statement);

        int Execute(string statement, object parameter);

        int Execute(string statement, object parameter, IDbTransaction transaction);
    }
}
