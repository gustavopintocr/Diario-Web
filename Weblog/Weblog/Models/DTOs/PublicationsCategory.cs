using System.ComponentModel.DataAnnotations.Schema;

namespace Weblog.Models.DTOs
{
    [NotMapped]

    public class PublicationsCategory
    {
        public List<Publication> Publications { get; set; } = new List<Publication>();
        public Category? Category;
    }
}
