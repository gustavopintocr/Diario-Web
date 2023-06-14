using System.ComponentModel.DataAnnotations.Schema;

namespace Weblog.Models.DTOs
{
    [NotMapped]
    public class PublicationWithAuthorInfo
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }
        public byte[]? Image { get; set; }
        public string? Body { get; set; }
        public string? AuthorId { get; set; }
        public string? AuthorName { get; set; }
    }
}
