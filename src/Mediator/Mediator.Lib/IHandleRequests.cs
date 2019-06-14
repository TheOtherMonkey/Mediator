using System.Threading.Tasks;

namespace Mediator
{
    /// <summary>
    /// Interface for classes that handle <see cref="IAmARequest{TResponse}"/>.
    /// </summary>
    /// <typeparam name="TRequest">The type that identifies the class that represents a request.</typeparam>
    /// <typeparam name="TResponse">The type that identifies the class that represents the response from the request.</typeparam>
    public interface IHandleRequests<in TRequest, TResponse> 
        where TRequest : IAmARequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request);
    }

    /// <summary>
    /// Interface for classes the handle <see cref="IAmARequest"/>.
    /// </summary>
    /// <typeparam name="TRequest">The type that identifies the class that represents a request.</typeparam>
    public interface IHandleRequests<in TRequest> 
        : IHandleRequests<TRequest, Void> 
        where TRequest : IAmARequest<Void>
    {}
}
