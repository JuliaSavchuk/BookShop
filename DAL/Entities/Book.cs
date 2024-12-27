using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Book
    {
        public int BookId { get; set; } // Первинний ключ
        public string Title { get; set; } // Назва книги
        public string Genre { get; set; } // Жанр книги
        public DateTime CreateBook { get; set; } //Рік написання книги
        public int AuthorId { get; set; } //Ідентифікатор автора. Зовнішній ключ
        public Author AuthorName { get; set; } //Автор книги (навігаційне поле)
        public decimal Price { get; set; }//Ціна книги
        public int QuantityInStock { get; set; }//Кількість книг в наявності
    }
}
