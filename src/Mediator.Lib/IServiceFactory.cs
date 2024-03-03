namespace Mediator
{
    /// <summary>
    /// Interface to be used by an object that wraps the IoC container of
    /// choice that will be used by the mediator.
    /// </summary>
    public interface IServiceFactory
    {
        /// <summary>
        /// Construct a new instance of the object that is of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of object to be constructed.</typeparam>
        /// <returns>An object of type <typeparamref name="T"/>.</returns>
        T GetInstance<T>();

        /// <summary>
        /// Get all instances of type T.
        /// </summary>
        /// <typeparam name="T">The type of object to be constructed.</typeparam>
        /// <returns>an enumerable list of items of type <typeparamref name="T"/>.</returns>
        IReadOnlyCollection<T> GetInstances<T>();
    }
}
