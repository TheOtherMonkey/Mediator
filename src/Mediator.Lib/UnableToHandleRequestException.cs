using Mediator.Properties;
using System;

namespace Mediator
{
    /// <summary>
    /// Exception for when the <see cref="IServiceFactory"/> is unable to provide an object that implements
    /// the <see cref="IHandleRequests{TRequest, TResponse}"/> or <see cref="IHandleRequests{TRequest}"/> interface
    /// when calling the <see cref="IMediate.Request{TResponse}(IAmARequest{TResponse})"/> or <see cref="IMediate.Send{TRequest}(TRequest)"/>
    /// methods.
    /// </summary>
    public class UnableToHandleRequestException : Exception
    {
        /// <summary>
        /// Construct a new instance of <see cref="UnableToHandleRequestException"/>.
        /// </summary>
        public UnableToHandleRequestException()
        {}

        /// <summary>
        /// Construct a new instance of <see cref="UnableToHandleRequestException"/>.
        /// </summary>
        /// <param name="message">The message for the exception.</param>
        public UnableToHandleRequestException(string message) 
            : base(message)
        {}

        /// <summary>
        /// Construct a new instance of <see cref="UnableToHandleRequestException"/>.
        /// </summary>
        /// <param name="message">The message for the exception.</param>
        /// <param name="innerException">The exception that was originally raised.</param>
        public UnableToHandleRequestException(string message, Exception innerException) 
            : base(message, innerException)
        {}

        /// <summary>
        /// Construct a new instance of <see cref="UnableToHandleRequestException"/>.
        /// </summary>
        /// <param name="requestType">The type of request for which a handler could not be created.</param>
        /// <param name="innerException">The exception that was originally raised.</param>
        public UnableToHandleRequestException(Type requestType, Exception innerException)
            : this(string.Format(Resources.UnableToHandleRequestExceptionText, requestType.Name), innerException)
        { }
    }
}
