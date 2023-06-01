using Microsoft.AspNetCore.Identity;

namespace Weblog.Models
{
    public class User : IdentityUser
    {
        public ICollection<Publication> Publications { get; } = new List<Publication>();
    }
}
