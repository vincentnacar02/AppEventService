namespace AppEventService
{
    public interface IAppEventDispatcher<T>
    {
        public string Key { get; }
        public void AddAction(Action<T> action);
        public void RemoveAction(Action<T> action);
        public void PublishEvent(T appEvent);
        public Task PublishEventAsync(T appEvent);
        public bool HasActions();
        public int CountActions();
    }
}
