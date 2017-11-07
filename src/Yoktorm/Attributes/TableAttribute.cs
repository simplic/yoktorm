using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoktorm.Db
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class TableAttribute : Attribute
    {
        public TableAttribute(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            private set;
        }
    }
}
