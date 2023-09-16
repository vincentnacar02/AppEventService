using Microsoft.Extensions.Logging;

namespace AppEventService
{
    public class AppEventDispatcher<T> : IAppEventDispatcher<T> where T : IAppEvent
    {
        private List<Action<T>> _actions = new List<Action<T>>();
        private string _key;
        private ILogger _logger;

        public string Key { get { return _key; } }

        public AppEventDispatcher(string key, ILogger logger)
        {
            _key = key;
            _logger = logger;
        }

        public void AddAction(Action<T> action)
        {
            _logger.LogInformation($"[{_key}] - action added.");
            _actions.Add(action);
        }

        public void RemoveAction(Action<T> action)
        {
            _logger.LogInformation($"[{_key}] - action removed.");
            _actions.Remove(action);
        }

        public void PublishEvent(T appEvent)
        {
            foreach (var action in _actions)
            {
                action(appEvent);
            }
            _logger.LogInformation($"[{_key}] - publish event. Total actions: {_actions.Count}");
        }

        public async Task PublishEventAsync(T appEvent)
        {
            var tasks = _actions.Select(a => Task.Run(() => a(appEvent)));
            await Task.WhenAll(tasks);
            _logger.LogInformation($"[{_key}] - publish event. Total actions: {_actions.Count}");
        }

        public bool HasActions()
        {
            return CountActions() > 0;
        }

        public int CountActions()
        {
            return _actions.Count;
        }
    }
}
