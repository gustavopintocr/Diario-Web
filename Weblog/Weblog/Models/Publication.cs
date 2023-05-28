namespace Weblog.Models
{
    public class Publication
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }
        public string? Header { get; set; }
        public byte[]? Image { get; set; }
        public string? Body { get; set; }
        public int NumComments { get; set; }
        public int IdAuthor { get; set; }
        public Author Author { get; set; } = null!;

    }
}
