using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoktorm.Db;

namespace SQLiteSample
{
    [Table(name:"Sample")] // Optional, without Table the engine will recognize the correct name too
    public interface ISample
    {
        string SampleText
        {
            get;
            set;
        }
    }
}
