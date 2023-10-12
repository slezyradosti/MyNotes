using Domain.Models;
using IndentityLogic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.EFInitial
{
    public class DataContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
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
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notebook>()
                .HasOne(e => e.Author)
                .WithMany(e => e.Notebooks)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notebook>()
                .HasMany(e => e.Units)
                .WithOne(e => e.Notebook)
                .HasForeignKey(e => e.NotebookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.Pages)
                .WithOne(e => e.Unit)
                .HasForeignKey(e => e.UnitId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Page>()
                .HasMany(e => e.Notes)
                .WithOne(e => e.Page)
                .HasForeignKey(e => e.PageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Note>()
                .HasMany(e => e.Photos)
                .WithOne(e => e.Note)
                .HasForeignKey(e => e.NoteId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
