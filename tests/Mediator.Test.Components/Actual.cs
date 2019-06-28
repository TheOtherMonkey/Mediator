using System;
using System.Collections.Generic;

namespace Mediator.Test.Components
{
    /// <summary>
    /// Static class containing the order list of calls that have been made through the
    /// </summary>
    public static class Actual
    {
        /// <summary>
        /// Queue containing the types of object that were called when a request is handled and the
        /// order in which they are called.
        /// </summary>
        public static readonly Queue<Type> Pipeline = new Queue<Type>();
    }
}
