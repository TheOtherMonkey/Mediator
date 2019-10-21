using System;
using System.Collections.Generic;

namespace Mediator.Test.Components
{
    /// <summary>
    /// Static class used to record the list of types that were involved in a test and the order in which they were called.
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
