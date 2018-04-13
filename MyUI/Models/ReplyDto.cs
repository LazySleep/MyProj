namespace MyUI.Models
{
    public class ReplyDto
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public string FromUId { get; set; }
        public string ToUId { get; set; }
        public string Content { get; set; }
        public string Time { get; set; }
    }
}