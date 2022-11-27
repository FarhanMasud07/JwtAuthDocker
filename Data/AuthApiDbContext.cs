using JwtAuthenticationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthenticationProject.Data
{
    public class AuthApiDbContext : DbContext
    {
        public AuthApiDbContext(DbContextOptions options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }

        public DbSet<QuestionType> QuestionTypes { get; set; }
    }
}
