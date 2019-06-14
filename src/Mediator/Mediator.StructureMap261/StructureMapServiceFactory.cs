using System.Collections.Generic;
using System.Linq;
using StructureMap;

namespace Mediator.StructureMap261
{
    /// <summary>
    /// Implementation of the <see cref="IServiceFactory"/> using a StructureMap
    /// <see cref="IContainer"/>.
    /// </summary>
    public class StructureMapServiceFactory : IServiceFactory
    {
        private readonly IContainer _container;

        /// <summary>
        /// Construct a new instance of the <see cref="StructureMapServiceFactory"/>.
        /// </summary>
        /// <param name="container">The StructureMap IoC container.</param>
        public StructureMapServiceFactory(IContainer container)
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
