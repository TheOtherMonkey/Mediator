namespace Aggregator
{
    /// <summary>
    /// Interface for an message that will be sent by <see cref="IAggregateMessages.Publish{T}"/>
    /// and handled by an object that implements the <see cref="IListenFor{T}"/> interface.
    /// </summary>
    public interface IAmAMessage
    {}
}
