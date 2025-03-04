using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoring.Server.DataAcces.Models
{
    public class Library
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        public bool IsOpen { get; set; }
        public virtual List<Book> Books { get; set; } = new List<Book>();
    }
}
