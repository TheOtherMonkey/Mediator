using System.Collections.Generic;
using System.Threading.Tasks;
using Aggregator;
using Mediator.Test.Components;
using Mediator.Test.Components.RequestHandlers;
using Mediator.Test.Components.Requests;
using Mediator.Test.Components.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StructureMap;

namespace Mediator.StructureMap
{
    [TestClass]
    public class MediatorIntegrationTests
    {
        private static Mock<IAggregateMessages> _mockAggregator;
        private static readonly Container Container = new Container(cfg =>
        {
            cfg.Scan(scanner =>
            {
                scanner.AssemblyContainingType<VoidRequest>();
                scanner.LookForRegistries();
                scanner.ConnectImplementationsToTypesClosing(typeof(IHandleRequests<,>));
            });

            cfg.For<IServiceFactory>().Use<StructureMapServiceFactory>();
            cfg.For<IAggregateMessages>().Use($"construct {nameof(IAggregateMessages)}", c =>
            {
                _mockAggregator = new Mock<IAggregateMessages>();
                return _mockAggregator.Object;
            });
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
        public async Task Request_GetAircraftQuery_Responds_With_Aircraft_List()
        {
            // Arrange
            var mediator = Container.GetInstance<Mediator>();

            // Act
            var response = await mediator
                .Request(new GetAircraftQuery())
                .ConfigureAwait(false);

            // Assert
            Assert.IsInstanceOfType(response, typeof(List<Aircraft>));
        }

        [TestMethod]
        public async Task Send_Request_Is_Handled()
        {
            // Arrange
            var mediator = Container.GetInstance<Mediator>();

            // Act 
            await mediator
                .Request(new VoidRequest())
                .ConfigureAwait(false);

            // Assert
            Assert.AreEqual(typeof(VoidRequestHandler), Actual.Pipeline.Peek());
        }

        [TestMethod]
        public async Task Publish_Calls_EventAggregator_SendMessage()
        {
            // Arrange
            var mockMessage = new Mock<IAmAMessage>().Object;
            var mediator = Container.GetInstance<Mediator>();

            // Act 
            await mediator
                .Publish(mockMessage)
                .ConfigureAwait(false);

            // Assert
            _mockAggregator.Verify(m => m.Publish(mockMessage));
        }
    }
}
