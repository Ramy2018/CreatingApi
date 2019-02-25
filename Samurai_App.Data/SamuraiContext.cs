using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    public class SamuraiContext:DbContext
    {
        public SamuraiContext(DbContextOptions<SamuraiContext> options)
            : base(options)
        { }

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnModelCreating
            (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>()
                .HasKey(s => new
                {
                    s.BattleId,
                    s.SamuraiId
                });
            modelBuilder.Entity<Samurai>().HasData(
                new {
                 Id=1,
                 Name="ramy"

                });
            modelBuilder.Entity<Quote>().HasData(
                new {
                 Id=1,
                 Text="great fighter",
                 SamuraiId=1

                },
                new
                {
                    Id=2,
                    Text="try hard",
                    SamuraiId=1
                }
                
                );
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //         "Server = (localdb)\\mssqllocaldb; Database = SamuraiAppData; Trusted_Connection = True; ");
        //}
    }
}
