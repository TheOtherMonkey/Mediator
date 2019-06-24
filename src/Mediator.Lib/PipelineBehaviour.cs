using System.Threading.Tasks;
// ReSharper disable UnusedParameter.Global

namespace Mediator
{
    /// <summary>
    /// Abstract base class for a component in the behaviour pipeline.
    /// </summary>
    /// <typeparam name="TRequest">The type that identifies the class that represents a request.</typeparam>
    /// <typeparam name="TResponse">The type that identifies the class that represents the response from the request.</typeparam>
    public abstract class PipelineBehaviour<TRequest, TResponse> 
        : IPipelineBehaviour<TRequest, TResponse> 
        where TRequest : IAmARequest<TResponse>
    {
        /// <summary>
        /// <inheritdoc cref="IPipelineBehaviour{TRequest,TResponse}"/>
        /// </summary>
        public async Task<TResponse> Handle(TRequest request, HandleRequestsDelegate<TResponse> next)
        {
            PreHandle(request);
            var response = await InvokeHandle(request, next);
            PostHandle(request, response);

            return response;
        }

        /// <summary>
        /// Perform actions prior the the next item in the pipeline being executed.
        /// </summary>
        /// <param name="request">The request that has been made.</param>
        protected virtual void PreHandle(TRequest request)
        { }

        /// <summary>
        /// Invoke the handler for the next item in the pipeline.
        /// </summary>
        /// <param name="request">The request that has been made.</param>
        /// <param name="next">The next item in the pipeline.</param>
        /// <returns>The response from the next item in the pipeline.</returns>
        // ReSharper disable once VirtualMemberNeverOverridden.Global
        protected virtual async Task<TResponse> InvokeHandle(TRequest request, HandleRequestsDelegate<TResponse> next)
        {
            return await next();
        }

        /// <summary>
        /// Perform actions after the the next item in the pipeline has been executed.
        /// </summary>
        /// <param name="request">The request that has been made.</param>
        /// <param name="response">The response from the next item in the pipeline.</param>
        protected virtual void PostHandle(TRequest request, TResponse response)
        { }
    }
}
