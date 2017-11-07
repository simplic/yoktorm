using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoktorm.Db
{
    /// <summary>
    /// Represents a column in a table / database structure
    /// </summary>
    public class ColumnStructure
    {
        /// <summary>
        /// Gets or sets the column name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets whether the column is part of the primary key
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// Gets or sets whether the column is nullable
        /// </summary>
        public bool NotNull { get; set; }

        /// <summary>
        /// Gets or sets the default value from the database
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets whether the default value is written as literal value
        /// </summary>
        public bool IsLiteralDefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the clr-type of the column
        /// </summary>
        public Type ColumnType { get; set; }
    }
}
