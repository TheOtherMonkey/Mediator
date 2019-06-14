using StructureMap;

namespace Mediator.StructureMap
{
    /// <summary>
    /// StructureMap registry for the <see cref="Mediator"/> objects.
    /// </summary>
    /// <remarks>
    /// In order for this registry to be used, the application that utilizes the
    /// registry must add the appropriate statements to the configuration of the
    /// StructureMap container that will then use this class to perform the registrations
    /// contained within.
    /// </remarks>
    public class MediatorRegistry : Registry
    {
        /// <summary>
        /// Construct a new instance of the <see cref="MediatorRegistry"/> class.
        /// </summary>
        /// <remarks>
        /// Used to registers the <see cref="StructureMapServiceFactory"/> as the default implementation for the
        /// <see cref="IServiceFactory"/>
        /// </remarks>
        public MediatorRegistry()
        {
            For<IServiceFactory>().Use<StructureMapServiceFactory>();
        }
    }
}
