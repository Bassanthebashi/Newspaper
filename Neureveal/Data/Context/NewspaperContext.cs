using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Neureveal.Data.Models;

namespace Neureveal.Data.Context
{
    public class NewspaperContext:DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public NewspaperContext(DbContextOptions<NewspaperContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable(name: "UserClaims");
            });
            builder.Entity<Category>().HasData(
                new Category { Id = Guid.NewGuid(), Name = "Science" },
                new Category { Id = Guid.NewGuid(), Name = "Programming" },
                new Category { Id = Guid.NewGuid(), Name = "MentalHealth" }
                );
            builder.Entity<Article>().HasData(
                new Article { Id = Guid.NewGuid(), Title = "Bones composition",
                Content="Minerals and spinal"},
                new Article { Id = Guid.NewGuid(), Title = "C++", Content="Not Pure OOP"},
                new Article { Id = Guid.NewGuid(), Title = "C#", Content="Pure OOP"}
                );
        }
    }
}
