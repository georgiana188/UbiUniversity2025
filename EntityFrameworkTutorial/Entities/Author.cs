using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkTutorial.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public Author() { this.Books = new List<Book>(); }
        public ICollection<Book> Books { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
