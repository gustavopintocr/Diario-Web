using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Models;

namespace Weblog.Data
{
    public class WeblogContext : DbContext
    {
        public WeblogContext (DbContextOptions<WeblogContext> options)
            : base(options)
        {
        }

        public DbSet<Weblog.Models.Publication> Publication { get; set; } = default!;

        public DbSet<Weblog.Models.Comment>? Comment { get; set; }

        public DbSet<Weblog.Models.User>? User { get; set; }

        public DbSet<Weblog.Models.Admin>? Admin { get; set; }

        public DbSet<Weblog.Models.Author>? Author { get; set; }
    }
}
