using Lamar;

namespace Mediator.Lamar
{
    /// <summary>
    /// Implementation of the <see cref="IServiceFactory"/> using a StructureMap
    /// <see cref="IContainer"/>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ServiceFactory : IServiceFactory
    {
        private readonly IContainer _container;

        /// <summary>
        /// Construct a new instance of the <see cref="ServiceFactory"/>.
        /// </summary>
        /// <param name="container">The StructureMap IoC container.</param>
        public ServiceFactory(IContainer container)
        {
            _container = container;
        }

        /// <inheritdoc cref="IServiceFactory"/>
        public T GetInstance<T>()
        {
            return _container.GetInstance<T>();
        }

        /// <inheritdoc cref="IServiceFactory"/>
        public IReadOnlyCollection<T> GetInstances<T>()
        {
            return _container.GetAllInstances<T>().ToList();
        }
    }
}
