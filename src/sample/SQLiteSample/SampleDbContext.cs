using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoktorm;

namespace SQLiteSample
{
    public class SampleDbContext : DbContext
    {
        internal SampleDbContext(IDatabase database, bool useObjectStateManager) 
            : base(database, useObjectStateManager)
        {

        }
    }
}
