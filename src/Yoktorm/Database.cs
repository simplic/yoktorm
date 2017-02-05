using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoktorm.Compilter;
using Yoktorm.Model;

namespace Yoktorm
{
    /// <summary>
    /// Datbase handler
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public abstract class Database<TContext> : IDbContext
    {
        private Model.IModelManager modelManager;
        private Compilter.IModelCompiler modelCompiler;
        private Statement.StatementCache statementCache;

        /// <summary>
        /// Initialize new database
        /// </summary>
        public Database()
        {
            modelManager = new Model.ModelManager();
            modelCompiler = new Compilter.ModelCompiler();
            statementCache = new Statement.StatementCache();
        }

        /// <summary>
        /// Initialize new database
        /// </summary>
        /// <param name="modelManager">Model manager</param>
        /// <param name="modelCompiler">Model compiler</param>
        public Database(Model.IModelManager modelManager, Compilter.IModelCompiler modelCompiler)
        {
            this.modelManager = modelManager;
            this.modelCompiler = modelCompiler;
            statementCache = new Statement.StatementCache();
        }

        /// <summary>
        /// Initialize the current database
        /// </summary>
        public void Initialize()
        {

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
