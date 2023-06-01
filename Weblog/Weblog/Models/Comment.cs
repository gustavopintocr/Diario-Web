namespace Weblog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Body { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PublicationId { get; set; }
        public Publication Publication { get; set; } = null!;
    }
}
