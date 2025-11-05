using EuroSkills2018.Models;
using Microsoft.EntityFrameworkCore;

namespace EuroSkills2018.Data
{
    public class ESDbContext : DbContext
    {
        public ESDbContext(DbContextOptions<ESDbContext> options) : base(options) { }

        public DbSet<Orszag> Orszagok { get; set; }
        public DbSet<Szakma> Szakmak { get; set; }
        public DbSet<Versenyzo> Versenyzok { get; set; }
    }
}
