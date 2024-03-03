namespace Aggregator
{
    /// <summary>
    /// Interface for a centralized in-memory Event Aggregator. 
    /// See https://martinfowler.com/eaaDev/EventAggregator.html for a description of the pattern.
    /// </summary>
    public interface IAggregateMessages
    {
        /// <summary>
        /// Add a new listener to the event aggregator. Each listener must implement the <see cref="IListenFor{T}"/> at least once
        /// to be added to the list of subscribers.
        /// </summary>
        /// <param name="listener">The listener to be added.</param>
        void AddListener(object listener);

        /// <summary>
        /// Asynchronously sends a message through the system to any registered object that implements the relevant
        /// .<see cref="IListenFor{T}"/> interface.
        /// </summary>
        /// <typeparam name="T">The type of message being raised.</typeparam>
        /// <param name="message">The message being sent.</param>        
        Task Publish<T>(T message) where T : IAmAMessage;
    }
}
