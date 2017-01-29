using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoktorm.SQLite
{
    public class SQLiteDbContext : DbContext<SQLiteConnection>
    {
        public SQLiteDbContext(string connectionString, bool useObjectStateManager) 
            : base(new SQLiteProvider(), new SQLiteConnectionHandler(), connectionString, useObjectStateManager)
        {

        }
    }
}
