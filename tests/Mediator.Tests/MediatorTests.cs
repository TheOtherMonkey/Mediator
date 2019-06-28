using System.Collections.Generic;
using System.Threading.Tasks;
using Aggregator;
using Mediator.Test.Components;
using Mediator.Test.Components.RequestHandlers;
using Mediator.Test.Components.Requests;
using Mediator.Test.Components.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Mediator.Tests
{
    /// <summary>
    /// Unit tests for the <see cref="Mediator"/> class.
    /// </summary>
    [TestClass]
    public class MediatorTests
    {
        private Mock<IAggregateMessages> _mockAggregator;
        private Mock<IServiceFactory> _moqFactory;
        private Mediator _target;

        /// <summary>
        /// Set up the test object.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            Actual.Pipeline.Clear();

            _mockAggregator = new Mock<IAggregateMessages>();            
            _moqFactory = new Mock<IServiceFactory>();            

            _target = new Mediator(_moqFactory.Object, _mockAggregator.Object);
        }

        [TestMethod]
        public async Task Request_Receive_Expected_Response()
        {
            // Arrange
            _moqFactory
                .Setup(m => m.GetInstance<IHandleRequests<GetAircraftQuery, List<Aircraft>>>())
                .Returns(new GetAircraftQueryHandler());

            _moqFactory.Setup(m => m.GetInstances<IPipelineBehaviour<GetAircraftQuery, List<Aircraft>>>())
                .Returns(new List<IPipelineBehaviour<GetAircraftQuery, List<Aircraft>>>());

            // Act
            var response = await _target
                .Request(new GetAircraftQuery())
                .ConfigureAwait(false);

            // Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task Request_GetFlightsQuery_Request_Handled()
        {
            // Arrange
            _moqFactory.Setup(m => m.GetInstance<IHandleRequests<GetFlightsQuery, List<Flight>>>())
                .Returns(new GetFlightsQueryHandler());

            _moqFactory.Setup(m => m.GetInstances<IPipelineBehaviour<GetFlightsQuery, List<Flight>>>())
                .Returns(new List<IPipelineBehaviour<GetFlightsQuery, List<Flight>>>());

            // Act
            await _target
                .Request(new GetFlightsQuery())
                .ConfigureAwait(false);

            // Assert
            Assert.AreEqual(typeof(GetFlightsQueryHandler), Actual.Pipeline.Peek());
        }

        [TestMethod]
        public async Task Send_Request_Is_Handled()
        {
            // Arrange
            _moqFactory.Setup(m => m.GetInstance<IHandleRequests<VoidRequest, Void>>())
                .Returns(new VoidRequestHandler());

            _moqFactory.Setup(m => m.GetInstances<IPipelineBehaviour<VoidRequest, Void>>())
                .Returns(new List<IPipelineBehaviour<VoidRequest, Void>>());

            // Act
            await _target
                .Send(new VoidRequest())
                .ConfigureAwait(false);

            // Assert
            Assert.AreEqual(typeof(VoidRequestHandler), Actual.Pipeline.Peek());
        }

        [TestMethod]
        public async Task Publish_Sends_Via_Message_Aggregator()
        {
            // Arrange
            var mockMessage = new Mock<IAmAMessage>().Object;

            // Act
            await _target.Publish(mockMessage).ConfigureAwait(false);

            // Assert
            _mockAggregator.Verify(m => m.Publish(mockMessage));
        }
    }
}
