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
        
        public DbSet<Book> Books { get; set; }


    }
}
