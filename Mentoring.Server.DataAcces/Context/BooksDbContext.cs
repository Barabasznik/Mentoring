using Mentoring.Server.DataAcces.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoring.Server.DataAcces.Context
{
    public class BooksDbContext : DbContext

    {
        public BooksDbContext(DbContextOptions options) : base(options)
        { 
            
        }

        public DbSet<Library> Library { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Library)
                .WithMany(l => l.Books)
                .HasForeignKey(b => b.LibraryId);
            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(50);
               

            modelBuilder.Entity<Book>()
                .Property(b => b.Author)
                .IsRequired();

        }
    }
}
