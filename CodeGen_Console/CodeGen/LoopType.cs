using System;

namespace QLike.CodeGen
{
    /// <summary>
    /// Type of loop
    /// </summary>
    internal enum LoopType
    {
        /// <summary>
        /// All columns
        /// </summary>
        All,

        /// <summary>
        /// All non-primary columns
        /// </summary>
        Primary,

        /// <summary>
        /// All primary keys
        /// </summary>
        Normal
    }
}
