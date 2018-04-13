namespace Model
{
    public class Reply
    {
        private int id = -1;
        private int commentId = -1;
        private string fromUId;
        private string toUId;
        private string content;
        private string time;

        public int Id { get => id; set => id = value; }
        public int CommentId { get => commentId; set => commentId = value; }
        public string FromUId { get => fromUId; set => fromUId = value; }
        public string ToUId { get => toUId; set => toUId = value; }
        public string Content { get => content; set => content = value; }
        public string Time { get => time; set => time = value; }
    }
}