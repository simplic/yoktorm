using System;
using System.Data;

namespace Yoktorm
{
    public interface IDatabase
    {
        Db.DatabaseStructure Structure
        {
            get;
        }

        IDatabaseProvider Provider
        {
            get;
        }
    }
}