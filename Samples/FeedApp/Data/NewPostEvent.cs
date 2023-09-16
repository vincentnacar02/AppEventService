using AppEventService;

namespace FeedApp.Data
{
    public class NewPostEvent : IAppEvent
    {
        public Post Post { get; }
        public NewPostEvent(Post post) {
            Post = post;
        }
    }
}
