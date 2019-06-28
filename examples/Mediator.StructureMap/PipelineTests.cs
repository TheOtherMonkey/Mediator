using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aggregator;
using Mediator.Test.Components;
using Mediator.Test.Components.RequestHandlerDecorators;
using Mediator.Test.Components.RequestHandlers;
using Mediator.Test.Components.Requests;
using Mediator.Test.Components.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StructureMap;

namespace Mediator.StructureMap
{
    /// <summary>
    /// Unit tests to test StructureMap's DecorateAllWith method to build
    /// a pipeline to intercept calls prior to invoking the
    /// <see cref="IHandleRequests{TRequest,TResponse}.Handle"/> method.
    /// </summary>
    [TestClass]
    public class PipelineTests
    {
        private static Mock<IAggregateMessages> _mockAggregator;

        private static readonly IContainer Container = new Container(cfg =>
        {
            cfg.Scan(scanner =>
            {
                scanner.AssemblyContainingType<VoidRequest>();
                scanner.AddAllTypesOf(typeof(IHandleRequests<,>));
            });

            var handler = cfg.For(typeof(IHandleRequests<,>));
            handler.DecorateAllWith(typeof(PreRequestDecorator<,>));
            handler.DecorateAllWith(typeof(PostRequestDecorator<,>));

            cfg.For<IServiceFactory>().Use<StructureMapServiceFactory>();
            cfg.For<IAggregateMessages>().Use($"construct {nameof(IAggregateMessages)}", c =>
            {
                _mockAggregator = new Mock<IAggregateMessages>();
                return _mockAggregator.Object;
            });

            cfg.For<IMediate>().Use<Mediator>();

        });

        /// <summary>
        /// Set up the test objects.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            Actual.Pipeline.Clear();
        }

        [TestMethod]
        public async Task TestPipeLine_Decorates_IHandleRequests_TRequest_TResponse()
        {
            // Arrange
            var expected = new Queue<Type>();
            expected.Enqueue(typeof(PreRequestDecorator<GetFlightsQuery, List<Flight>>));
            expected.Enqueue(typeof(GetFlightsQueryHandler));
            expected.Enqueue(typeof(PostRequestDecorator<GetFlightsQuery, List<Flight>>));

            var mediator = Container.GetInstance<IMediate>();

            // Act
            await mediator.Request(new GetFlightsQuery());

            // Assert
            CollectionAssert.AreEqual(expected, Actual.Pipeline);
        }

        [TestMethod]
        public async Task TestPipeLine_Decorates_IHandleRequests_TRequest()
        {
            // Arrange
            var expected = new Queue<Type>();
            expected.Enqueue(typeof(PreRequestDecorator<VoidRequest, Void>));
            expected.Enqueue(typeof(VoidRequestHandler));
            expected.Enqueue(typeof(PostRequestDecorator<VoidRequest, Void>));

            var mediator = Container.GetInstance<Mediator>();

            // Act
            await mediator.Send(new VoidRequest());

            // Assert
            CollectionAssert.AreEqual(expected, Actual.Pipeline);
        }
    }
}
