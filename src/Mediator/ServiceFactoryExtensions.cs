using System;
using System.Collections.Generic;

namespace Mediator
{
    /// <summary>
    /// Static extension methods for the <see cref="IServiceFactory"/> interface.
    /// </summary>
    internal static class ServiceFactoryExtensions
    {
        /// <summary>
        /// Get the <see cref="IHandleRequests{TRequest,TResponse}"/> for the <typeparamref name="TRequest"/>
        /// </summary>
        /// <typeparam name="TRequest">The type of request.</typeparam>
        /// <typeparam name="TResponse">The type response returned from the request.</typeparam>
        /// <param name="factory"></param>
        /// <returns>An instance of the class that implements the relevant <see cref="IHandleRequests{TRequest,TResponse}"/> interface.</returns>
        public static IHandleRequests<TRequest, TResponse> GetRequestHandler<TRequest, TResponse>(this IServiceFactory factory)
            where TRequest : IAmARequest<TResponse>
        {
            try
            {
                return factory.GetInstance<IHandleRequests<TRequest, TResponse>>();
            }
            catch (Exception e)
            {
                throw new UnableToHandleRequestException(typeof(TRequest), e);
            }
        }

        /// <summary>
        /// Get the list of <see cref="IPipelineBehaviour{TRequest,TResponse}"/> that have been identified to be executed when
        /// fulfilling the <typeparamref name="TRequest"/>.
        /// </summary>
        /// <typeparam name="TRequest">The type of request.</typeparam>
        /// <typeparam name="TResponse">The type response returned from the request.</typeparam>
        /// <param name="factory">The implementation of the <see cref="IServiceFactory"/>.</param>
        /// <returns>The list of <see cref="IPipelineBehaviour{TRequest,TResponse}"/> that will be executed to fulfill the request.</returns>
        public static IReadOnlyCollection<IPipelineBehaviour<TRequest, TResponse>> 
            GetPipeLineBehaviors<TRequest, TResponse>(this IServiceFactory factory)
            where TRequest : IAmARequest<TResponse>
        {
            return factory.GetInstances<IPipelineBehaviour<TRequest, TResponse>>();
        }
    }
}
