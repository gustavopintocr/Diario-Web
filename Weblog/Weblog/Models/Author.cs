using System.ComponentModel.DataAnnotations.Schema;

namespace Weblog.Models
{
    [Table("Author")]
    public class Author : User
    {
        public ICollection<Publication> Publications { get;} = new List<Publication>();
    }
}
