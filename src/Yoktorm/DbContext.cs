using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoktorm.ObjectState;
using Dapper;

namespace Yoktorm
{
    /// <summary>
    /// Yoktorm database context. This context manage all statement executions, connections, model states and is the public interface of the orm
    /// </summary>
    /// <typeparam name="TConnection">ADO.Net Connection class of the ado.net provider that should be used</typeparam>
    public abstract class DbContext : IDbContext, IDisposable
    {
        #region Fields
        private ObjectState.IObjectStateManager objectStateManager;
        private IDbConnection connection;
        private IDatabase database;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize new database context
        /// </summary>
        /// <param name="provider">Provider instance</param>
        /// <param name="connectionString">Connection string</param>
        /// <param name="useObjectStateManager">Defines whether to use the object state manager or not</param>
        protected DbContext(IDatabase database, bool useObjectStateManager)
        {
            this.database = database;

            if (useObjectStateManager)
            {
                this.objectStateManager = CreateObjectStateManager();
            }

            InitializeDatabase();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Ensure that the current connection is instantiated and opened
        /// </summary>
        private void EnsureConnection()
        {
            if (connection == null)
                connection = database.GetConnection(true);
        }
        #endregion

        #region Public Methods

        #region [Dispose]
        /// <summary>
        /// Dispose the current DbContext and close all connections
        /// </summary>
        public virtual void Dispose()
        {
            if (connection != null)
                connection.Dispose();
        }
        #endregion


        private void InitializeDatabase()
        {
            EnsureConnection();

            // Try to initialize the database
            database.Initialize();
        }

        #region [Query]
        public virtual IEnumerable<T> Query<T>() where T : new()
        {
            return Query<T>(null);
        }

        public virtual IEnumerable<T> Query<T>(object parameter) where T : new()
        {
            return Query<T>(null, parameter);
        }

        public virtual IEnumerable<T> Query<T>(string statement, object parameter) where T : new()
        {
            return Query<T>(statement, parameter, null);
        }

        public virtual IEnumerable<T> Query<T>(string statement, object parameter, IDbTransaction transaction) where T : new()
        {
            EnsureConnection();
            return new List<T>();
        }

        // public virtual IEnumerable<T> Query<T, TParent>(object parameter) where T : new()
        // {
        // Query child data by usign foreignkey information
        // }

        public IEnumerable<T> QueryPoco<T>(string statement, object parameter = null, IDbTransaction transaction = null)
        {
            EnsureConnection();

            return QueryHelper.QueryPoco<T>(connection, statement, parameter, transaction);
        }

        public virtual IEnumerable<dynamic> Query(string statement, object parameter = null, IDbTransaction transaction = null)
        {
            EnsureConnection();

            return QueryHelper.Query(connection, statement, parameter, transaction);
        }
        #endregion

        #region [Execute]
        public virtual int Execute(string statement)
        {
            return Execute(statement, null);
        }

        public virtual int Execute(string statement, object parameter)
        {
            return Execute(statement, parameter, null);
        }

        public virtual int Execute(string statement, object parameter, IDbTransaction transaction)
        {
            EnsureConnection();

            return QueryHelper.Execute(connection, statement, parameter, transaction);
        }
        #endregion

        #region [Add]
        public void Add<T>(T obj) where T : new()
        {

        }

        public void Add<T>(IEnumerable<T> objs) where T : new()
        {

        }
        #endregion

        #region [Delete]
        public void Delete<T>(T obj) where T : new()
        {

        }

        public void Delete<T>(IEnumerable<T> objs) where T : new()
        {

        }
        #endregion

        #region [Factory Methods]
        /// <summary>
        /// Create an instance of a class, that inherits from <see cref="IObjectStateManager"/>
        /// </summary>
        /// <returns>Instance of an <see cref="IObjectStateManager"/></returns>
        protected virtual IObjectStateManager CreateObjectStateManager()
        {
            return new ObjectState.ObjectStateManager();
        }
        #endregion

        #region [Context operations]
        /// <summary>
        /// Save changes to the database
        /// </summary>
        public void SaveChanges()
        {
            if (objectStateManager == null)
                throw new Exception("No ObjectStateManager exists in the current context. Set useObjectStateManager to true when creating a new DbContext.");
        }
        #endregion

        #endregion

        #region Public Member
        /// <summary>
        /// Gets the object state manager
        /// </summary>
        public IObjectStateManager ObjectStateManager
        {
            get
            {
                return objectStateManager;
            }
        }
        #endregion
    }
}
