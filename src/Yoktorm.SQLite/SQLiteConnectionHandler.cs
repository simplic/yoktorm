using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoktorm.SQLite
{
    public class SQLiteConnectionHandler : IConnectionHandler<SQLiteConnection>
    {
        public SQLiteConnection Get(string connectionString)
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
