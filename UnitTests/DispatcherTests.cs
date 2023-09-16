using AppEventService;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{

    [TestClass]
    public class DispatcherTests
    {
        [TestMethod]
        public void CreateDispatcherAction()
        {
            var mockLogger = new Mock<ILogger<IAppEventService<TestEvent>>>();

            var dispatcher = new AppEventDispatcher<TestEvent>(string.Empty, mockLogger.Object);

            dispatcher.AddAction((ev) =>
            {
                Console.WriteLine(ev);
            });

            Assert.AreEqual(1, dispatcher.CountActions());
        }

        [TestMethod]
        public void CreateDispatcherAction_Multiple()
        {
            var mockLogger = new Mock<ILogger<IAppEventService<TestEvent>>>();

            var dispatcher = new AppEventDispatcher<TestEvent>(string.Empty, mockLogger.Object);

            dispatcher.AddAction((ev) =>
            {
                Console.WriteLine(ev);
            });
            dispatcher.AddAction((ev) =>
            {
                Console.WriteLine(ev);
            });

            Assert.AreEqual(2, dispatcher.CountActions());
        }

        [TestMethod]
        public void RemoveDispatcherAction()
        {
            var mockLogger = new Mock<ILogger<IAppEventService<TestEvent>>>();

            var dispatcher = new AppEventDispatcher<TestEvent>(string.Empty, mockLogger.Object);

            Action<TestEvent> action = (ev) =>
            {
                Console.WriteLine(ev);
            };
            dispatcher.AddAction(action);

            dispatcher.RemoveAction(action);

            Assert.AreEqual(0, dispatcher.CountActions());
        }

        [TestMethod]
        public void Publish_NotifyAction()
        {
            var mockLogger = new Mock<ILogger<IAppEventService<TestEvent>>>();

            var dispatcher = new AppEventDispatcher<TestEvent>(string.Empty, mockLogger.Object);

            bool hasNotified = false;
            Action<TestEvent> action = (ev) =>
            {
                hasNotified = true;
            };
            dispatcher.AddAction(action);

            dispatcher.PublishEvent(new TestEvent());

            Assert.IsTrue(hasNotified);
        }

        [TestMethod]
        public void Publish_NotifyMultipleAction()
        {
            var mockLogger = new Mock<ILogger<IAppEventService<TestEvent>>>();

            var dispatcher = new AppEventDispatcher<TestEvent>(string.Empty, mockLogger.Object);

            bool action1Notified = false;
            bool action2Notified = false;
            Action<TestEvent> action1 = (ev) =>
            {
                action1Notified = true;
            };
            Action<TestEvent> action2 = (ev) =>
            {
                action2Notified = true;
            };
            dispatcher.AddAction(action1);
            dispatcher.AddAction(action2);

            dispatcher.PublishEvent(new TestEvent());

            Assert.IsTrue(action1Notified);
            Assert.IsTrue(action2Notified);
        }

        [TestMethod]
        public async Task PublishAsync_NotifyAction()
        {
            var mockLogger = new Mock<ILogger<IAppEventService<TestEvent>>>();

            var dispatcher = new AppEventDispatcher<TestEvent>(string.Empty, mockLogger.Object);

            bool hasNotified = false;
            Action<TestEvent> action = (ev) =>
            {
                hasNotified = true;
            };
            dispatcher.AddAction(action);

            await dispatcher.PublishEventAsync(new TestEvent());

            Assert.IsTrue(hasNotified);
        }

        [TestMethod]
        public async Task PublishAsync_NotifyMultipleAction()
        {
            var mockLogger = new Mock<ILogger<IAppEventService<TestEvent>>>();

            var dispatcher = new AppEventDispatcher<TestEvent>(string.Empty, mockLogger.Object);

            bool action1Notified = false;
            bool action2Notified = false;
            Action<TestEvent> action1 = (ev) =>
            {
                action1Notified = true;
            };
            Action<TestEvent> action2 = (ev) =>
            {
                action2Notified = true;
            };
            dispatcher.AddAction(action1);
            dispatcher.AddAction(action2);

            await dispatcher.PublishEventAsync(new TestEvent());

            Assert.IsTrue(action1Notified);
            Assert.IsTrue(action2Notified);
        }
    }
}
