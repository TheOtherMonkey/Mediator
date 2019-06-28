using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aggregator;
using Mediator.Test.Components;
using Mediator.Test.Components.PipelineBehaviours;
using Mediator.Test.Components.RequestHandlers;
using Mediator.Test.Components.Requests;
using Mediator.Test.Components.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StructureMap;

namespace Mediator.StructureMap261
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
                scanner.LookForRegistries();
                scanner.ConnectImplementationsToTypesClosing(typeof(IHandleRequests<,>));
            });

            cfg.For<IServiceFactory>().Use<StructureMapServiceFactory>();
            cfg.For(typeof(IPipelineBehaviour<,>)).Add(typeof(PreRequestPipelineBehaviour<,>));
            cfg.For(typeof(IPipelineBehaviour<,>)).Add(typeof(PostRequestPipelineBehaviour<,>));

            cfg.For<IAggregateMessages>().Use(c =>
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
            expected.Enqueue(typeof(PreRequestPipelineBehaviour<GetAircraftQuery, List<Aircraft>>));
            expected.Enqueue(typeof(GetAircraftQueryHandler));
            expected.Enqueue(typeof(PostRequestPipelineBehaviour<GetAircraftQuery, List<Aircraft>>));
            var mediator = Container.GetInstance<IMediate>();

            // Act
            await mediator.Request(new GetAircraftQuery());

            // Assert
            CollectionAssert.AreEqual(expected, Actual.Pipeline);
        }

        [TestMethod]
        public async Task TestPipeLine_Decorates_IHandleRequests_TRequest()
        {
            // Arrange
            var expected = new Queue<Type>();
            expected.Enqueue(typeof(PreRequestPipelineBehaviour<VoidRequest, Void>));
            expected.Enqueue(typeof(VoidRequestHandler));
            expected.Enqueue(typeof(PostRequestPipelineBehaviour<VoidRequest, Void>));
            var mediator = Container.GetInstance<Mediator>();

            // Act
            await mediator.Send(new VoidRequest());

            // Assert
            CollectionAssert.AreEqual(expected, Actual.Pipeline);
        }
    }
}
