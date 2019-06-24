// ReSharper disable UnusedTypeParameter
namespace Mediator
{
    /// <summary>
    /// Interface for a class that represents a request that will be made using 
    /// <see cref="IMediate.Send{TRequest}"/> and handled by an object that
    /// implements the <see cref="IHandleRequests{TRequest, TResponse}"/> interface.
    /// </summary>
    public interface IAmARequest : IAmARequest<Void>
    {}

    /// <summary>
    /// Interface for a class that represents a request that will be made using 
    /// <see cref="IMediate.Request{TResponse}"/> and the type of object that is returned
    /// as its response. Will be handled by an object that implements the
    /// <see cref="IHandleRequests{TRequest, TResponse}"/> interface.
    /// </summary>
    /// <typeparam name="TResponse">The type that identifies the class that represents the response from the request.</typeparam>
    public interface IAmARequest<out TResponse> 
    {}
}
