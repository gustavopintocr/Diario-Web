namespace Weblog.Models.DTOs
{
    public class PublicationsCategory
    {
        public List<Publication> Publications { get; set; } = new List<Publication>();
        public Category? Category;
    }
}
