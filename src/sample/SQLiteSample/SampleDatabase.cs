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
        private string connectionString;

        public SampleDatabase(string connectionString) : base(new SQLiteProvider())
        {
            this.connectionString = connectionString;
        }

        public override SampleDbContext Create()
        {
            return new SampleDbContext(this, connectionString, true);
        }
    }
}
