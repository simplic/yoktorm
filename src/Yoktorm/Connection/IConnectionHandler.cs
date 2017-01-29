using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoktorm
{
    public interface IConnectionHandler<TConnection> where TConnection : IDbConnection
    {
        TConnection Get(string connectionString);
    }
}
