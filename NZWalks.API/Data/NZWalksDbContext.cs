using Microsoft.EntityFrameworkCore;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext:DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions):base (dbContextOptions)
        {
                
        }
        public DbSet<Difficulity> difficulities { get; set; }
        public DbSet<Region> regions { get; set; }  
        public DbSet<Walk>walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //seed data for difficulties
            //easy , medium, gard
            //ModelBuilder.Equals(optionsBuilder, nameof(ModelBuilder));
            //var difficulties==new List<Difficulity>()
            //{
            //    new Difficulity()
            //    {
            //        Id=
            //    }
            //}
        }

        //public override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
