using System.ComponentModel.DataAnnotations.Schema;

namespace Weblog.Models.DTOs
{
    [NotMapped]

    public class PublicationsAuthor
    {
        public List<Publication> Publications { get; set; } = new List<Publication>();
        public User? Author;
    }
}
