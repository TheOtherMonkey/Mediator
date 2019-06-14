using System.Threading.Tasks;

namespace Mediator
{
    /// <summary>
    /// Represents a null/void response for a <see cref="IAmARequest{TResponse}"/>
    /// </summary>
    public class Void
    {
        public static readonly Task<Void> Task = System.Threading.Tasks.Task.FromResult(Instance);

        public static Void Instance => new Void();

        private Void()
        { }
    }
}
