using AnimesLand.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimesLand.Infrastructure.Persistence
{
    public class AnimeDbContext : DbContext
    {
        public AnimeDbContext(DbContextOptions<AnimeDbContext> options) 
            : base(options) { }

        public DbSet<Anime> Animes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
