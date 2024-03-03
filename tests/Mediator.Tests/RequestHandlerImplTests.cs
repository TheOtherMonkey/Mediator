using Mediator.Test.Components;
using Mediator.Test.Components.PipelineBehaviours;
using Mediator.Test.Components.RequestHandlers;
using Mediator.Test.Components.Requests;
using Mediator.Test.Components.Responses;
using Moq;

namespace Mediator.Tests
{
    /// <summary>
    /// Unit tests for the <see cref="RequestHandlerImpl{TRequest,TResponse}"/> class.
    /// </summary>
    [TestClass]
    public class RequestHandlerImplTests
    {
        private Mock<IServiceFactory> _mockFactory;
        private List<IPipelineBehaviour<TestRequest, TestResponse>> _pipelineBehaviours;
        private RequestHandlerImpl<TestRequest, TestResponse> _target;

        /// <summary>
        /// set up the test objects.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            Actual.Pipeline.Clear();
            _pipelineBehaviours = new List<IPipelineBehaviour<TestRequest, TestResponse>>();

            _mockFactory = new Mock<IServiceFactory>();
            _mockFactory.Setup(m => m.GetInstance<IHandleRequests<TestRequest, TestResponse>>())
                .Returns(new TestRequestHandler());
            _mockFactory.Setup(m => m.GetInstances<IPipelineBehaviour<TestRequest, TestResponse>>())
                .Returns(_pipelineBehaviours);

            _target = new RequestHandlerImpl<TestRequest, TestResponse>();
        }

        /// <summary>
        /// Verify the response from the <see cref="RequestHandlerImpl{TRequest,TResponse}.Handle"/>
        /// is the not null
        /// </summary>
        [TestMethod]
        public async Task Handle_Result_Is_Not_Null()
        {
            // Arrange

            // Act
            var actual = await _target.Handle(new TestRequest(), _mockFactory.Object);

            //Assert
            Assert.IsNotNull(actual);
        }

        /// <summary>
        /// Verify the <see cref="RequestHandlerImpl{TRequest,TResponse}.Handle"/> requests the
        /// <see cref="IHandleRequests{TRequest,TResponse}"/> from the <see cref="IServiceFactory"/>
        /// </summary>
        [TestMethod]
        public async Task Handle_RequestHandler_Is_Created_Using_IServiceLocator()
        {
            // Arrange

            // Act
            await _target.Handle(new TestRequest(), _mockFactory.Object);

            //Assert
            _mockFactory.Verify(m => m.GetInstance<IHandleRequests<TestRequest, TestResponse>>());
        }

        /// <summary>
        /// Verify the <see cref="RequestHandlerImpl{TRequest,TResponse}.Handle"/> requests the
        /// <see cref="IPipelineBehaviour{TRequest,TResponse}"/> objects from the <see cref="IServiceFactory"/>.
        /// </summary>
        [TestMethod]
        public async Task Handle_Creates_PipeLineBehaviour_Using_IServiceFactory()
        {
            // Arrange

            // Act
            await _target.Handle(new TestRequest(), _mockFactory.Object);

            // Assert
            _mockFactory.Verify(m => m.GetInstances<IPipelineBehaviour<TestRequest, TestResponse>>());
        }

        /// <summary>
        /// Verify the <see cref="RequestHandlerImpl{TRequest,TResponse}.Handle"/> calls
        /// <see cref="IPipelineBehaviour{TRequest,TResponse}.Handle"/> on the registered
        /// <see cref="IPipelineBehaviour{TRequest,TResponse}"/> objects.
        /// </summary>
        [TestMethod]
        public async Task Handle_Request_Handled_By_PipelineBehaviour()
        {
            // Arrange
            _pipelineBehaviours.Add(new PreRequestPipelineBehaviour<TestRequest, TestResponse>());

            //Act
            await _target.Handle(new TestRequest(), _mockFactory.Object);

            // Assert
            Assert.AreEqual(typeof(PreRequestPipelineBehaviour<TestRequest, TestResponse>), Actual.Pipeline.Peek());
        }

        /// <summary>
        /// Verify the <see cref="RequestHandlerImpl{TRequest,TResponse}.Handle"/> calls
        /// <see cref="IPipelineBehaviour{TRequest,TResponse}.Handle"/> on each of the registered
        /// <see cref="IPipelineBehaviour{TRequest,TResponse}"/> objects in the expected order.
        /// </summary>
        [TestMethod]
        public async Task Handle_Request_Passed_Through_Pipeline_In_Expected_Order()
        {
            // Arrange
            var expected = new Queue<Type>();
            expected.Enqueue(typeof(PreRequestPipelineBehaviour<TestRequest, TestResponse>));
            expected.Enqueue(typeof(TestRequestHandler));
            expected.Enqueue(typeof(PostRequestPipelineBehaviour<TestRequest, TestResponse>));

            _pipelineBehaviours.Add(new PreRequestPipelineBehaviour<TestRequest, TestResponse>());
            _pipelineBehaviours.Add(new PostRequestPipelineBehaviour<TestRequest, TestResponse>());

            //Act
            await _target.Handle(new TestRequest(), _mockFactory.Object);

            // Assert
            CollectionAssert.AreEqual(expected, Actual.Pipeline);
        }
    }
}
