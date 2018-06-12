using game2048.Models;
using Microsoft.EntityFrameworkCore;

namespace game2048.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<ScoreClass> Scores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScoreClass>().ToTable("Score");
        }
    }
}