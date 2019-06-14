using System;

namespace Mediator
{
    /// <summary>
    /// Static extension methods for the <see cref="IServiceFactory"/> interface.
    /// </summary>
    internal static class ServiceFactoryExtensions
    {
        /// <summary>
        /// Wrap the call to the <see cref="IServiceFactory.GetInstance{T}"/> with an exception handler.
        /// </summary>
        /// <typeparam name="T">The type of object to be constructed.</typeparam>
        /// <param name="factory">The <see cref="IServiceFactory"/> being extended.</param>
        /// <returns>An object of type <typeparamref name="T"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown when an instance of type <typeparamref name="T"/>
        /// cannot be constructed.</exception>
        public static T GetHandler<T>(this IServiceFactory factory)
        {
            try
            {
                return factory.GetInstance<T>();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Unable to construct handler for request of type {typeof(T)}", e);
            }
        }
    }
}
