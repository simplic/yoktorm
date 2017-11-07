using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoktorm.Model
{
    public abstract class TableBase : ITable
    {
        private IList<string> trackedColumns;
        private bool enableChangeTracking;

        public abstract string TableName { get; }

        public abstract void Fill(IDataReader reader);

        public bool EnableChangeTracking
        {
            get
            {
                return enableChangeTracking;
            }
            set
            {
                enableChangeTracking = value;
                if (enableChangeTracking && trackedColumns == null)
                {
                    trackedColumns = new List<string>();
                }
            }
        }
        
        public IList<string> TrackedColumns
        {
            get
            {
                return trackedColumns;
            }
        }
    }
}
