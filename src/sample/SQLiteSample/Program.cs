using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup yoktorm
            var database = new SampleDatabase("Data Source=Sample.db;Version=3;New=True;");

            // Setup a context
            using (var context = database.Create())
            {
                context.Execute("CREATE TABLE IF NOT EXISTS Sample (SampleText VARCHAR(100) NOT NULL PRIMARY KEY)");

                var random = new Random(DateTime.Now.Millisecond);
                var value = new { val = $"sample-value {random.Next()}"};

                var addResult = context.Execute("INSERT OR REPLACE INTO Sample(SampleText) VALUES (@val)", value);
                Console.WriteLine($"Insert result: {addResult}");

                foreach (dynamic dyn in context.Query("SELECT * FROM Sample"))
                    Console.WriteLine($" SampleText: {dyn.SampleText}");
            }

            Console.ReadLine();
        }
    }
}
