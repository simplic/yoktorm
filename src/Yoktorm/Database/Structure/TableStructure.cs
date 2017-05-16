using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoktorm.Db
{
    /// <summary>
    /// Represents a table in a database
    /// </summary>
    public class TableStructure
    {
        /// <summary>
        /// Gets or sets the user that owns the table
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the table scheme
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Gets or sets the table name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the columns in the table
        /// </summary>
        private IList<ColumnStructure> Columns { get; set; } = new List<ColumnStructure>();
    }
}
