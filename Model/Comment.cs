namespace Model
{
    public class Comment
    {
        private int id = -1;
        private string articleId;
        private string fromUId;
        private string content;
        private string time;

        public int Id { get => id; set => id = value; }
        public string ArticleId { get => articleId; set => articleId = value; }
        public string FromUId { get => fromUId; set => fromUId = value; }
        public string Content { get => content; set => content = value; }
        public string Time { get => time; set => time = value; }
    }
}