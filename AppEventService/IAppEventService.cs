namespace AppEventService
{
    public interface IAppEventService<T>
    {
        public IAppEventDispatcher<T> GetOrCreateDispatcher(string key);
        public void DestroyDispatcher(string key);
        public int CountDispatcher();
    }
}
