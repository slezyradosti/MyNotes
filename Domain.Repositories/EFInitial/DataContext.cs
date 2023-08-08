using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.EFInitial
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {          
        }

        public DbSet<Notebook> Notebooks { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notebook>()
                .HasMany(e => e.Units)
                .WithOne(e => e.Notebook)
                .HasForeignKey(e => e.Id)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.Pages)
                .WithOne(e => e.Unit)
                .HasForeignKey(e => e.Id)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Page>()
                .HasMany(e => e.Notes)
                .WithOne(e => e.Page)
                .HasForeignKey(e => e.Id)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
