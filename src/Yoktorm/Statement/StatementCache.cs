using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoktorm.Statement
{
    internal class StatementCache
    {
        private IDictionary<string, string> selectStatemetns;
        private IDictionary<string, string> insertStatements;
        private IDictionary<string, string> updateStatements;
        private IDictionary<string, string> deleteStatements;
    }
}
