using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }  // Унікальний ідентифікатор автора
        public string Name { get; set; }  // Ім'я автора
        public List<Book> Books { get; set; }  // Список книг автора
    }
}
