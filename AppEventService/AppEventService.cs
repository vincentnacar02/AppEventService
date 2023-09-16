using Microsoft.Extensions.Logging;

namespace AppEventService
{
    public class AppEventService<T> : IAppEventService<T> where T : IAppEvent
    {
        private Dictionary<string, IAppEventDispatcher<T>> _dispatchers;
        private ILogger _logger;

        public AppEventService(ILogger<IAppEventService<T>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dispatchers = new Dictionary<string, IAppEventDispatcher<T>>();
        }

        public IAppEventDispatcher<T> GetOrCreateDispatcher(string key)
        {
            if (_dispatchers.ContainsKey(key))
            {
                return _dispatchers[key];
            }
            var newDispatcher = new AppEventDispatcher<T>(key, _logger);
            _dispatchers[key] = newDispatcher;
            _logger.LogInformation($"event dispatcher created: {key}");
            return newDispatcher;
        }

        public void DestroyDispatcher(string key)
        {
            if (_dispatchers.ContainsKey(key))
            {
                var dispatcher = _dispatchers[key];
                if (!dispatcher.HasActions())
                {
                    _dispatchers.Remove(key);
                    _logger.LogInformation($"event dispatcher removed: {key}. Active dispatchers: {_dispatchers.Count}");
                }
            }
        }

        public int CountDispatcher()
        {
            return _dispatchers.Count;
        }
    }
}