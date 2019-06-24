using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable InconsistentNaming

namespace Aggregator.Tests
{
    /// <summary>
    /// Unit tests for the <see cref="MessageAggregator"/>
    /// </summary>
    [TestClass]
    public class MessageAggregatorTests
    {
        private MessageAggregator _target;

        private ListenerA _listenerA;
        private ListenerAB _listenerAB;

        /// <summary>
        /// Set up the test objects.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _target = new MessageAggregator();

            _listenerA = new ListenerA();
            _listenerAB = new ListenerAB();
        }

        /// <summary>
        /// Verify that a listener can be added to the <see cref="MessageAggregator"/>.
        /// </summary>
        [TestMethod]
        public void AddListener_Adds_Listener()
        {
            _target.AddListener(_listenerA);

            _target.Subscriptions.TryGetValue(typeof(IListenFor<ObservableA>), out var subscribers);

            Assert.IsNotNull(subscribers);
            Assert.IsTrue(subscribers.Count == 1);
        }

        /// <summary>
        /// Verify that a listener is only added to the <see cref="MessageAggregator"/> once.
        /// </summary>
        [TestMethod]
        public void AddListener_SameListener_AddedOnce()
        {
            _target.AddListener(_listenerA);
            _target.AddListener(_listenerA);

            _target.Subscriptions.TryGetValue(typeof(IListenFor<ObservableA>), out var subscribers);

            Assert.IsNotNull(subscribers);
            Assert.IsTrue(subscribers.Count == 1);
        }

        /// <summary>
        /// Verify that all of the <see cref="IListenFor{T}"/> interfaces implemented by an object are
        /// subscribed to.
        /// </summary>
        [TestMethod]
        public void AddListener_All_Subscriptions_Added()
        {
            _target.AddListener(_listenerAB);

            Assert.IsTrue(_target.Subscriptions.Keys.Count == 2);
        }

        /// <summary>
        /// Verify that a listener that does not implement the <see cref="IListenFor{T}"/> interface is not added.
        /// </summary>
        [TestMethod]
        public void AddListener_Does_Not_Add_Non_IListener()
        {
            _target.AddListener(new ObservableB());

            Assert.IsFalse(_target.Subscriptions.Any());
        }


        /// <summary>
        /// Verify the <see cref="MessageAggregator.Publish{T}(T)"/> sends to all subscribers.
        /// </summary>
        [TestMethod]
        public async Task Publish_Sends_To_All_Subscribers()
        {
            _target.AddListener(_listenerA);
            _target.AddListener(_listenerAB);

            await _target.Publish(new ObservableA()).ConfigureAwait(false);

            Assert.AreEqual(1, _listenerA.MessageAReceived, "ListenerA");
            Assert.AreEqual(1, _listenerAB.MessageAReceived, "ListenerAB");
        }

        /// <summary>
        /// Verify message is sent to all registered <see cref="IListenFor{T}"/> interfaces.
        /// </summary>
        [TestMethod]
        public async Task Publish_Sends_To_All_Registered_Interfaces()
        {
            _target.AddListener(_listenerAB);

            await _target.Publish(new ObservableA()).ConfigureAwait(false);
            await _target.Publish(new ObservableB()).ConfigureAwait(false);

            Assert.AreEqual(1, _listenerAB.MessageAReceived);
            Assert.AreEqual(1, _listenerAB.MessageBReceived);
        }

        /// <summary>
        /// Verify the <see cref="MessageAggregator.Publish{T}(T)"/> automatically removes zombie
        /// references.
        /// </summary>
        [TestMethod]
        public async Task Publish_Removes_Zombie_References()
        {
            _target.AddListener(_listenerA);
            _target.AddListener(_listenerAB);

            _listenerA = null; // De-reference the listener
            GC.Collect(0); // Force the Garbage Collector into action.

            await _target.Publish(new ObservableA()).ConfigureAwait(false);

            _target.Subscriptions.TryGetValue(typeof(IListenFor<ObservableA>), out var subscribers);

            Assert.IsNotNull(subscribers);
            Assert.AreEqual(1, subscribers.Count);
        }
    }

    #region Internal Test Classes
    [ExcludeFromCodeCoverage]
    internal class ObservableA : IAmAMessage
    {}

    [ExcludeFromCodeCoverage]
    internal class ObservableB : IAmAMessage
    { }

    [ExcludeFromCodeCoverage]
    internal class ListenerA : IListenFor<ObservableA>
    {
        public int MessageAReceived { get; private set; }

        public Task Handle(ObservableA message)
        {
            MessageAReceived++;
            return Task.CompletedTask;
        }
    }

    [ExcludeFromCodeCoverage]
    internal class ListenerAB : IListenFor<ObservableA>, IListenFor<ObservableB>
    {

        public int MessageAReceived { get; private set; }

        public int MessageBReceived { get; private set; }

        public Task Handle(ObservableA message)
        {
            MessageAReceived++;
            return Task.CompletedTask;
        }

        public Task Handle(ObservableB message)
        {
            MessageBReceived++;
            return  Task.CompletedTask;
        }
    }
    #endregion
}
