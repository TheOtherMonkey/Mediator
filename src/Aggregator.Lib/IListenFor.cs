using System.Threading.Tasks;
// ReSharper disable UnusedParameter.Global

namespace Aggregator
{
    /// <summary>
    /// Generic interface to identify a class's interest in a particular message that can be propagated
    /// by the <see cref="IAggregateMessages"/>. Objects can implement multiple <see cref="IListenFor{T}"/>
    /// interfaces if they are interested in multiple messages.    
    /// </summary>
    /// <typeparam name="T">The type of message being listened for.</typeparam>
    /// <remarks>
    /// To be automatically register new objects with an application's <see cref="IAggregateMessages"/>,
    /// the <see cref="IAggregateMessages"/> needs to be registered as a singleton object and a StructureMap
    /// type interceptor is required (assuming StructureMap is the IoC of choice).
    /// </remarks>
    public interface IListenFor<in T> where T : IAmAMessage
    {
        /// <summary>
        /// Respond to the particular message being raised.
        /// </summary>
        /// <param name="message">The message that was raised.</param>
        Task Handle(T message);
    }
}