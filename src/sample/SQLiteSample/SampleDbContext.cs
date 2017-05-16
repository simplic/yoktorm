using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoktorm;

namespace SQLiteSample
{
    public class SampleDbContext : DbContext<SQLiteConnection>
    {
        internal SampleDbContext(IDatabase database, string connectionString, bool useObjectStateManager) 
            : base(database, connectionString, useObjectStateManager)
        {

        }
    }
}
