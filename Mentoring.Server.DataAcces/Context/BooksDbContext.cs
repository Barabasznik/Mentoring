using Microsoft.EntityFrameworkCore;
using Mentoring.Domain.Models;

namespace Mentoring.Server.DataAcces.Context
{
    public class BooksDbContext : DbContext
    {
        public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Book>()
                .Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = -1, Title = "Czysty Kod", Description = "Podręcznik dla programistów", Author = "Robert C. Martin" },
                new Book { Id = -2, Title = "Refaktoryzacja", Description = "Poprawianie struktury kodu", Author = "Martin Fowler" },
                new Book { Id = -3, Title = "Algorytmy", Description = "Wprowadzenie do algorytmiki", Author = "Thomas H. Cormen" },
                new Book { Id = -4, Title = "Pragmatyczny Programista", Description = "Droga do mistrzostwa", Author = "Andrew Hunt" }
            );

            modelBuilder.Entity<Borrowing>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Borrowing>()
                .HasOne(b => b.Book)
                .WithMany()
                .HasForeignKey(b => b.BookId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Book)
                .WithMany()
                .HasForeignKey(r => r.BookId);

         

        }
    }
}
