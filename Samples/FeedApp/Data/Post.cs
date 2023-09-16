namespace FeedApp.Data
{
    public class Post
    {
        public string PostId { get; set; }
        public string PostedBy { get; set; }
        public string PostBody { get; set; }
        public int LikeCount { get; set; }
    }
}
