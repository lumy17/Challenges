using Challenges.WebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Challenges.WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("challenges");
            base.OnModelCreating(builder);
        }

        public DbSet<Challenge> Challenge { get; set; } = default!;
        public DbSet<UserChallenge> UserChallenge { get; set; } = default!;
        public DbSet<AppUser> AppUser { get; set; } = default!;
        public DbSet<Badge> Badge { get; set; } = default!;
        public DbSet<UserBadge> UserBadge { get; set; } = default!;
        public DbSet<TodoTask> TodoTask { get; set; } = default!;
        public DbSet<FinishedTask> FinishedTask { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<ChallengeCategory> ChallengeCategory { get; set; } = default!;
        public DbSet<UserPreference> UserPreference { get; set; } = default!;
    }
}