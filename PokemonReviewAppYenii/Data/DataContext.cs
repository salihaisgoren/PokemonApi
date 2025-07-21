using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using PokemonReviewAppYenii.Model;

namespace PokemonReviewAppYenii.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners  { get; set; }
        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<PokemonOwner> PokemonOwners { get; set; }
        public DbSet<PokemonCategory> PokemonCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<PokemonFood> PokemonFoods { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<Arm> Arms { get; set; }
        public DbSet<Move> Moves { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PokemonCategory>()
                .HasKey(pc => new { pc.PokemonId, pc.CategoryId });

            modelBuilder.Entity<PokemonCategory>()
                .HasOne(p => p.Pokemon)
                .WithMany(pc => pc.PokemonCategories)
                 .HasForeignKey(p => p.PokemonId);

            modelBuilder.Entity<PokemonCategory>()
               .HasOne(p => p.Category)
               .WithMany(pc => pc.PokemonCategories)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<PokemonOwner>()
               .HasKey(pc => new { pc.PokemonId, pc.OwnerId });


            modelBuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Pokemon)
                .WithMany(po => po.PokemonOwners)
                 .HasForeignKey(p => p.PokemonId);

            modelBuilder.Entity<PokemonOwner>()
               .HasOne(p => p.Owner)
               .WithMany(po => po.PokemonOwners)
                .HasForeignKey(c => c.OwnerId);

            modelBuilder.Entity<PokemonFood>()
               .HasKey(pf => new { pf.PokemonId, pf.FoodId });

            modelBuilder.Entity<PokemonFood>()
                .HasOne(p => p.Pokemon)
                .WithMany(pf => pf.PokemonFoods)
                 .HasForeignKey(p => p.PokemonId);

            modelBuilder.Entity<Colour>()
                          .HasOne(p => p.Pokemon)
                          .WithMany(pc => pc.Colours)
                           .HasForeignKey(c => c.PokemonId);

            modelBuilder.Entity<Arm>()
                         .HasOne(p => p.Pokemon)
                         .WithMany(pa => pa.Arms)
                          .HasForeignKey(a => a.PokemonId);
        }
    }
}
