namespace Weblog.Models.DTOs
{
    public class Homepage
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Publication> Publications { get; set; } = new List<Publication>();
        public List<User> Authors { get; set; } = new List<User>();
    }
}
