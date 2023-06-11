namespace Weblog.Models.DTOs
{
    public class PublicationsAuthor
    {
        public List<Publication> Publications { get; set; } = new List<Publication>();
        public User? Author;
    }
}
