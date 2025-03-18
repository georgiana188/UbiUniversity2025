using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkTutorial.Entities
{
    public class Category
    {
        public Category()
        {
            this.Books = new List<Book>();
            this.Locations = new List<Location>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual IList<Book> Books { get; set; }
        public virtual IList<Location> Locations { get; set; }
    }
}
