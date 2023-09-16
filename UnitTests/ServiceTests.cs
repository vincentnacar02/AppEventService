using AppEventService;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class ServiceTests
    {
        [TestMethod]
        public void CreateService()
        {
            var mockLogger = new Mock<ILogger<IAppEventService<TestEvent>>>();
            var appService = new AppEventService<TestEvent>(mockLogger.Object);

            var dispatcher = appService.GetOrCreateDispatcher(string.Empty);
            Assert.IsNotNull(dispatcher);
        }

        [TestMethod]
        public void CreateService_EnsureNoDuplicateDispatcher()
        {
            var mockLogger = new Mock<ILogger<IAppEventService<TestEvent>>>();
            var appService = new AppEventService<TestEvent>(mockLogger.Object);

            var dispatcher1 = appService.GetOrCreateDispatcher(string.Empty);
            var dispatcher2 = appService.GetOrCreateDispatcher(string.Empty);

            Assert.IsNotNull(dispatcher1);
            Assert.IsNotNull(dispatcher2);
            Assert.AreEqual(1, appService.CountDispatcher());
        }

        [TestMethod]
        public void CreateService_MultipleDispatcher()
        {
            var mockLogger = new Mock<ILogger<IAppEventService<TestEvent>>>();
            var appService = new AppEventService<TestEvent>(mockLogger.Object);

            var dispatcher1 = appService.GetOrCreateDispatcher(Guid.NewGuid().ToString());
            var dispatcher2 = appService.GetOrCreateDispatcher(Guid.NewGuid().ToString());

            Assert.IsNotNull(dispatcher1);
            Assert.IsNotNull(dispatcher2);
            Assert.AreEqual(2, appService.CountDispatcher());
        }

        [TestMethod]
        public void DestroyDispatcher()
        {
            var mockLogger = new Mock<ILogger<IAppEventService<TestEvent>>>();
            var appService = new AppEventService<TestEvent>(mockLogger.Object);

            var dispatcher1 = appService.GetOrCreateDispatcher(Guid.NewGuid().ToString());
            var dispatcher2 = appService.GetOrCreateDispatcher(Guid.NewGuid().ToString());

            appService.DestroyDispatcher(dispatcher1.Key);
            appService.DestroyDispatcher(dispatcher2.Key);

            Assert.AreEqual(0, appService.CountDispatcher());
        }
    }
}