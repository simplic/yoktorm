using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoktorm.Db
{
    /// <summary>
    /// Represents a foreignkey in the database
    /// </summary>
    public class ForeignKeyStructure
    {
        /// <summary>
        /// Gets or sets the source table
        /// </summary>
        public TableStructure SourceTable { get; set; }

        /// <summary>
        /// Gets or sets the target table
        /// </summary>
        public TableStructure TargetTable { get; set; }

        /// <summary>
        /// Gets or sets the columns for the foreignkey
        /// </summary>
        public IList<Tuple<ColumnStructure, ColumnStructure>> Columns { get; set; } = new List<Tuple<ColumnStructure, ColumnStructure>>();

        /// <summary>
        /// Gets or sets the name of the foreign key
        /// </summary>
        public string Name { get; set; }
    }
}
