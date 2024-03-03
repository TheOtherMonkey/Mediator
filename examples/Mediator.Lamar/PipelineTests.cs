using Aggregator;
using Lamar;
using Lamar.Scanning.Conventions;
using Mediator.Test.Components;
using Mediator.Test.Components.PipelineBehaviours;
using Mediator.Test.Components.RequestHandlerDecorators;
using Mediator.Test.Components.RequestHandlers;
using Mediator.Test.Components.Requests;
using Mediator.Test.Components.Responses;
using Moq;

namespace Mediator.Lamar
{
    /// <summary>
    /// Unit tests to test StructureMap's DecorateAllWith method to build
    /// a pipeline to intercept calls prior to invoking the
    /// <see cref="IHandleRequests{TRequest,TResponse}.Handle"/> method.
    /// </summary>
    [TestClass]
    public class PipelineTests
    {

        /// <summary>
        /// Set up the test objects.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            Actual.Pipeline.Clear();
        }


        [TestMethod]
        public async Task Request_Uses_Registered_Decorator_RequestHandlers()
        {
            // Arrange
            var container = new Container(cfg =>
            {
                cfg.Scan(scanner =>
                {
                    scanner.AssemblyContainingType<VoidRequest>();
                    scanner.ConnectImplementationsToTypesClosing(typeof(IHandleRequests<,>));
                });

                var handler = cfg.For(typeof(IHandleRequests<,>));
                handler.DecorateAllWith(typeof(PreRequestDecorator<,>));
                handler.DecorateAllWith(typeof(PostRequestDecorator<,>));

                cfg.For<IAggregateMessages>().Use(c => new Mock<IAggregateMessages>().Object);

                cfg.For<IMediate>().Use<Mediator>();
                cfg.For<IServiceFactory>().Use<ServiceFactory>();
            });

            var expected = new Queue<Type>();
            expected.Enqueue(typeof(PreRequestDecorator<GetAircraftQuery, List<Aircraft>>));
            expected.Enqueue(typeof(GetAircraftQueryHandler));
            expected.Enqueue(typeof(PostRequestDecorator<GetAircraftQuery, List<Aircraft>>));

            var mediator = container.GetInstance<IMediate>();

            // Act
            await mediator.Request(new GetAircraftQuery())
                .ConfigureAwait(false);
            
            // Assert
            CollectionAssert.AreEqual(expected, Actual.Pipeline);
        }
    }
}
