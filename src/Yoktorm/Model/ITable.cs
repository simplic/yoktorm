using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoktorm.Model
{
    public interface ITable
    {
        void Fill(IDataReader reader);
        string TableName { get; }
        bool EnableChangeTracking { get; set; }
        IList<string> TrackedColumns { get; }
    }
}
