﻿using System.Threading.Tasks;
using Aggregator;
using Lamar;
using Mediator.Test.Components.Handlers;
using Mediator.Test.Components.Pipeline;
using Mediator.Test.Components.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        private static Mock<IAggregateMessages> _mockAggregator;

        private static readonly IContainer Container = new Container(cfg =>
        {
            cfg.Scan(scanner =>
            {
                scanner.AssemblyContainingType<VoidRequest>();
                scanner.ConnectImplementationsToTypesClosing(typeof(IHandleRequests<,>));
            });

            cfg.For(typeof(IPipelineBehaviour<,>)).Add(typeof(AuthorizationPipeline<,>));
            cfg.For(typeof(IPipelineBehaviour<,>)).Add(typeof(LoggingPipeline<,>));

            cfg.For<IAggregateMessages>().Use(c =>
            {
                _mockAggregator = new Mock<IAggregateMessages>();
                return _mockAggregator.Object;
            });

            cfg.For<IMediate>().Use<Mediator>();
            cfg.For<IServiceFactory>().Use<ServiceFactory>();
        });

        [TestMethod]
        public async Task TestPipeLine_Decorates_IHandleRequests_TRequest_TResponse()
        {
            // Arrange
            var mediator = Container.GetInstance<IMediate>();

            // Act
            await mediator.Request(new GetAircraftQuery());

            // Assert
            Assert.IsTrue(LoggingHandler.RequestHandled);
            Assert.IsTrue(AuthorizationHandler.RequestHandled);
            Assert.IsTrue(GetAircraftQueryHandler.RequestHandled);
        }

        [TestMethod]
        public async Task TestPipeLine_Decorates_IHandleRequests_TRequest()
        {
            // Arrange
            var mediator = Container.GetInstance<Mediator>();

            // Act
            await mediator.Send(new VoidRequest());

            // Assert
            Assert.IsTrue(LoggingHandler.RequestHandled);
            Assert.IsTrue(AuthorizationHandler.RequestHandled);
            Assert.IsTrue(VoidRequestHandler.RequestHandled);
        }
    }
}