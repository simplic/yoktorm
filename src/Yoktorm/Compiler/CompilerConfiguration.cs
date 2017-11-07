using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yoktorm.Compiler
{
    public class CompilerConfiguration
    {
        public string Namespace
        {
            get;
            set;
        }

        public string AssemblyName
        {
            get;
            set;
        }

        public List<Assembly> References
        {
            get;
            set;
        }
    }
}
