namespace Weblog.Models
{
    public class Author
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<Publication> Publications { get;} = new List<Publication>();
    }
}
