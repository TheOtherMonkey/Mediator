// ReSharper disable UnusedMember.Global

namespace Mediator
{
    /// <summary>
    /// Represents a null/void response for a <see cref="IAmARequest{TResponse}"/>
    /// </summary>
    public class Void
    {
        /// <summary>
        /// Get a new <see cref="Task{Void}"/> instance.
        /// </summary>
        public static readonly Task<Void> Task = System.Threading.Tasks.Task.FromResult(Instance);

        /// <summary>
        /// Get the instance of the <see cref="Void"/> object.
        /// </summary>
        public static Void Instance => new Void();

        /// <summary>
        /// Private constructor to prevent new instances of the <see cref="Void"/> object from being
        /// instantiated.
        /// </summary>
        private Void()
        { }
    }
}
