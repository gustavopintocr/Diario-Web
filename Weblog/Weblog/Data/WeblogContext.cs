using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Weblog.Models;

namespace Weblog.Data
{
    public class WeblogContext : IdentityDbContext<User>
    {
        public WeblogContext (DbContextOptions<WeblogContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "Author", NormalizedName = "AUTHOR" }
            );

            modelBuilder.Entity<Publication>().ToTable("Publication");

            modelBuilder.Entity<Comment>().ToTable("Comment");

            modelBuilder.Entity<Category>().ToTable("Category");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Weblog.Models.Publication> Publication { get; set; } = default!;
        public DbSet<Weblog.Models.Comment> Comment { get; set; }
        public DbSet<Weblog.Models.Category> Category { get; set; }
    }
}
