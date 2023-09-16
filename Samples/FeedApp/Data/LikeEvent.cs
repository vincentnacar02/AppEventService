using AppEventService;

namespace FeedApp.Data
{
    public class LikeEvent : IAppEvent
    {
        public string PostId { get; }
        public string LikedBy { get; }
        public LikeEvent(string postId, string likedBy) {
            PostId = postId;
            LikedBy = likedBy;
        }
    }
}
