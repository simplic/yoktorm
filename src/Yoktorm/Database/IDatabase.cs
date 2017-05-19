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

        void Initialize(bool force = false);

        /// <summary>
        /// Get a new connection instance of the specific provider
        /// </summary>
        /// <param name="ensureIsOpen">If set to true, the connection will be opened</param>
        /// <returns>Instance of a connection</returns>
        IDbConnection GetConnection(bool ensureIsOpen = false);
    }
}