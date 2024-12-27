﻿using System;
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


//Проект Книжковий Магазин

//Має існує такий функціонал:

//1)Додавання/Редагування Книг (У окремих вікнах після натискання кнопки у головному вікні)
//2)Видалення Книг
//3)Додавання/Редагування Авторів (У окремих вікнах після натискання кнопки у головному вікні)
//4)Видалення Авторів
//5)Користувач може вибирати автора з комбобоксу, який заповнюється з бази даних.
//6)Пошук книги за Автором/Назвою/Жанром/Роком написання/Ціною
//7)Сортування за Автором/Назвою/Жанром/Роком написання/Ціною/Кількістю книг в наявності
//8)Виведення всіх книг одного автора
