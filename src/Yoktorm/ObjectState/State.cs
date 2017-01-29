using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoktorm.ObjectState
{
    /// <summary>
    /// Represents a state of a model
    /// </summary>
    public enum State
    {
        /// <summary>
        /// No specific state
        /// </summary>
        None,

        /// <summary>
        /// Newly added to the context
        /// </summary>
        Added,

        /// <summary>
        /// The model was modified
        /// </summary>
        Modified,

        /// <summary>
        /// The object was deleted
        /// </summary>
        Deleted
    }
}
