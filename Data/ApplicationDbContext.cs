using Challenges.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Challenges.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Provocare> Provocare { get; set; } = default!;
        public DbSet<ProvocareUtilizator> ProvocareUtilizator { get; set; } = default!;
        public DbSet<Utilizator> Utilizator { get; set; } = default!;
        public DbSet<Realizare> Realizare { get; set; } = default!;
        public DbSet<RealizareUtilizator> RealizareUtilizator { get; set; } = default!;
        public DbSet<Sarcina> Sarcina { get; set; } = default!;
        public DbSet<SarcinaRealizata> SarcinaRealizata { get; set; } = default!;
        public DbSet<VizualizareProvocare> VizualizareProvocare { get; set; } = default!;
        public DbSet<Categorie> Categorie { get; set; } = default!;
        public DbSet<CategorieProvocare> CategorieProvocare { get; set; } = default!;
        public DbSet<CategorieUtilizator> CategorieUtilizator { get; set; } = default!;
    }
}