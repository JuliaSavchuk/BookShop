using DAL.Entities;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookShop.AdditionalWindows
{
    /// <summary>
    /// Логика взаимодействия для AddAuthorWindow.xaml
    /// </summary>
    public partial class AddAuthorWindow : Window
    {
        private BookstoreContext _context = new BookstoreContext();
        public AddAuthorWindow()
        {
            InitializeComponent();
        }

        private void SaveAuthor_Click(object sender, RoutedEventArgs e)
        {
            var authorName = AuthorNameTextBox.Text.Trim();

            // Перевірка, чи введено ім'я автора
            if (string.IsNullOrEmpty(authorName))
            {
                MessageBox.Show("Будь ласка, введіть ім'я автора.");
                return;
            }

            var newAuthor = new Author
            {
                Name = authorName
            };

            // Додаємо нового автора в базу даних
            _context.Authors.Add(newAuthor);
            _context.SaveChanges();

            MessageBox.Show("Автор успішно доданий!");

            // Закриваємо вікно після додавання
            this.Close();
        }
    }
}
