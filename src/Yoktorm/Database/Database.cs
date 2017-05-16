using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        private object _lock = new object();
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize new database
        /// </summary>
        /// <param name="provider">Provider instance</param>
        public Database(TProvider provider)
        {
            modelManager = new Model.ModelManager();
            modelCompiler = new Compiler.ModelCompiler();
            this.provider = provider;

            SetupCache();
        }

        /// <summary>
        /// Initialize new database
        /// </summary>
        /// <param name="provider">Provider instance</param>
        /// <param name="modelManager">Model manager</param>
        /// <param name="modelCompiler">Model compiler</param>
        public Database(TProvider provider, Model.IModelManager modelManager, Compiler.IModelCompiler modelCompiler)
        {
            this.modelManager = modelManager;
            this.modelCompiler = modelCompiler;
            this.provider = provider;

            SetupCache();
        }
        #endregion

        public void Initialize(IDynamicDbContext context, bool force = false)
        {
            lock (_lock)
            {
                if (!isInitialized || force)
                {
                    isInitialized = true;
                    structure = provider.GetStructure(context);
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
        /// Preregistere an interface
        /// </summary>
        /// <typeparam name="T">Interface to register</typeparam>
        public void Registet<T>()
        {
            // Precompile models
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
