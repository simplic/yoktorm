using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoktorm;
using Yoktorm.SQLite;

namespace SQLiteSample
{
    public class SampleDatabase : Database<SampleDbContext, SQLiteProvider>
    {
        public SampleDatabase(string connectionString) : base(connectionString, new SQLiteProvider())
        {
            
        }

        public override SampleDbContext Create()
        {
            return new SampleDbContext(this, true);
        }
    }
}
