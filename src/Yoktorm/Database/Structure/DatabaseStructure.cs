using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoktorm.Db
{
    /// <summary>
    /// Represents a complete database structure. This class must be serializeable
    /// </summary>
    public class DatabaseStructure
    {
        private IList<TableStructure> tables;
        private IList<ForeignKeyStructure> foreignKeys;
        
        /// <summary>
        /// Initialize structure
        /// </summary>
        public DatabaseStructure()
        {
            tables = new List<TableStructure>();
            foreignKeys = new List<ForeignKeyStructure>();
        }

        /// <summary>
        /// Gets or sets a list of tables inside the database
        /// </summary>
        public IList<TableStructure> Tables
        {
            get
            {
                return tables;
            }

            set
            {
                tables = value;
            }
        }

        /// <summary>
        /// Gets or sets the foreign keys in the database
        /// </summary>
        public IList<ForeignKeyStructure> ForeignKeys
        {
            get
            {
                return foreignKeys;
            }

            set
            {
                foreignKeys = value;
            }
        }
    }
}
