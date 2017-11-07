using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yoktorm.Compiler
{
    public interface IModelCompiler
    {
        byte[] Compile(Db.DatabaseStructure structure);
    }
}
