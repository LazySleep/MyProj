namespace MyUI.Models
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string ArticleId { get; set; }
        public string FromUId { get; set; }
        public string Content { get; set; }
        public string Time { get; set; }
    }
}