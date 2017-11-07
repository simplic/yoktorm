using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yoktorm.Compiler;
using Yoktorm.Db;
using Yoktorm.Model;

namespace Yoktorm
{
    /// <summary>
    /// Datbase handler
    /// </summary>
    /// <typeparam name="TContext">Context type</typeparam>
    /// <typeparam name="TProvider">Provider type</typeparam>
    public abstract class Database<TContext, TProvider> : IDatabase
        where TProvider : IDatabaseProvider
        where TContext : IDbContext
    {
        #region Fields
        private Model.IModelManager modelManager;
        private Compiler.IModelCompiler modelCompiler;
        private Statement.StatementCache statementCache;
        private Db.DatabaseStructure structure;
        private TProvider provider;
        private volatile bool isInitialized;
        private string connectionString;
        private object _lock = new object();
        private IInstanceCreator creator;
        private Assembly assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize new database
        /// </summary>
        /// <param name="provider">Provider instance</param>
        /// <param name="connectionString">Connection string</param>
        public Database(string connectionString, TProvider provider)
        {
            modelManager = GetModelManager();
            modelCompiler = GetModelCompiler();
            this.provider = provider;
            this.connectionString = connectionString;
            SetupCache();
        }
        #endregion

        protected virtual IModelManager GetModelManager()
        {
            return new ModelManager();
        }

        protected virtual IModelCompiler GetModelCompiler()
        {
            var configuration = new CompilerConfiguration()
            {
                AssemblyName = $"{GetType().Name}",
                Namespace = $"{GetType().Namespace.Replace(".", "_")}"
            };

            return new ModelCompiler(configuration);
        }

        public IInstanceCreator GetInstanceCreator()
        {
            if (creator == null)
            {
                // Instantiate creator
                var type = typeof(IInstanceCreator);
                var creatorType = assembly.GetTypes()
                    .Where(p => type.IsAssignableFrom(p)).FirstOrDefault();

                return (IInstanceCreator)Activator.CreateInstance(creatorType);
            }

            return creator;
        }

        public void Initialize(bool force = false)
        {
            lock (_lock)
            {
                if (!isInitialized || force)
                {
                    isInitialized = true;
                    structure = provider.GetStructure(GetConnection(true));

                    // Compile and load assembly
                    var asmBytes = modelCompiler.Compile(structure);
                    assembly = Assembly.Load(asmBytes);

                    creator = GetInstanceCreator();
                }
            }
        }

        /// <summary>
        /// Initialize the caching system
        /// </summary>
        private void SetupCache()
        {
            statementCache = new Statement.StatementCache();
        }

        /// <summary>
        /// Register a data table interface
        /// </summary>
        /// <param name="_interface">Type of interface</param>
        public void RegisterTable(Type _interface)
        {
            if (_interface == null) throw new ArgumentNullException(nameof(_interface));
            if (!_interface.IsInterface) throw new ArgumentOutOfRangeException("T");

            var tableName = GetTableName(_interface);
        }

        /// <summary>
        /// Preregistere an interface
        /// </summary>
        /// <typeparam name="T">Interface to register</typeparam>
        public void RegisterTable<T>()
        {
            RegisterTable(typeof(T));
        }

        /// <summary>
        /// Get a new connection instance of the specific provider
        /// </summary>
        /// <param name="ensureIsOpen">If set to true, the connection will be opened</param>
        /// <returns>Instance of a connection</returns>
        public IDbConnection GetConnection(bool ensureIsOpen = false)
        {
            var connection = provider.GetConnection(connectionString);
            if (ensureIsOpen && connection.State != ConnectionState.Open)
                connection.Open();

            return connection;
        }

        /// <summary>
        /// Gets the table name by the type-name
        /// </summary>
        /// <param name="type">Type to get the table-name from. If the type ha an TableAttribute, use it</param>
        /// <returns></returns>
        private string GetTableName(Type type)
        {
            // If the type has a TableAttribute, use it
            var tableAttribute = type.GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() as TableAttribute;

            if (tableAttribute != null && !string.IsNullOrWhiteSpace(tableAttribute.Name))
                return tableAttribute.Name.Trim();

            // Interfaces often starts with I (e.g. IUser), so lets remove it
            if (type.Name.StartsWith("I"))
                return type.Name.Remove(0, 1);

            // Simply return the type name
            return type.Name;
        }

        /// <summary>
        /// Create a new <see cref="DbContext"/> instance
        /// </summary>
        /// <returns>Returns a new instance <see cref="IDbContext"/> instance</returns>
        public abstract TContext Create();

        /// <summary>
        /// Gets the current model manager
        /// </summary>
        protected IModelManager ModelManager
        {
            get
            {
                return modelManager;
            }
        }

        /// <summary>
        /// Gets the current model compiler
        /// </summary>
        protected IModelCompiler ModelCompiler
        {
            get
            {
                return modelCompiler;
            }
        }

        /// <summary>
        /// Gets or sets the database structure
        /// </summary>
        public DatabaseStructure Structure
        {
            get
            {
                return structure;
            }

            set
            {
                structure = value;
            }
        }

        /// <summary>
        /// Gets the provider instance
        /// </summary>
        public IDatabaseProvider Provider
        {
            get
            {
                return provider;
            }
        }
    }
}
