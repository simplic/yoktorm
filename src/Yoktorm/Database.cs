using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoktorm.Compiler;
using Yoktorm.Model;

namespace Yoktorm
{
    /// <summary>
    /// Datbase handler
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public abstract class Database<TContext> : IDbContext, IDatabase
    {
        private Model.IModelManager modelManager;
        private Compiler.IModelCompiler modelCompiler;
        private Statement.StatementCache statementCache;

        /// <summary>
        /// Initialize new database
        /// </summary>
        public Database()
        {
            modelManager = new Model.ModelManager();
            modelCompiler = new Compiler.ModelCompiler();

            SetupCache();
        }

        /// <summary>
        /// Initialize new database
        /// </summary>
        /// <param name="modelManager">Model manager</param>
        /// <param name="modelCompiler">Model compiler</param>
        public Database(Model.IModelManager modelManager, Compiler.IModelCompiler modelCompiler)
        {
            this.modelManager = modelManager;
            this.modelCompiler = modelCompiler;

            SetupCache();
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
    }
}
