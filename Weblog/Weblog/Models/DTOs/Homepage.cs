using System.ComponentModel.DataAnnotations.Schema;

namespace Weblog.Models.DTOs
{
    [NotMapped]

    public class Homepage
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<PublicationWithAuthorInfo> Publications { get; set; } = new List<PublicationWithAuthorInfo>();
        public List<User> Authors { get; set; } = new List<User>();
    }
}
