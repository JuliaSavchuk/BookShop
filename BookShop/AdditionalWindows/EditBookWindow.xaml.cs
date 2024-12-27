using DAL;
using DAL.Entities;
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
    /// Логика взаимодействия для EditBookWindow.xaml
    /// </summary>
    public partial class EditBookWindow : Window
    {
        private Book _bookToEdit;
        private BookstoreContext _context;

        public EditBookWindow(Book book)
        {
            InitializeComponent();
            _bookToEdit = book;
            _context = new BookstoreContext();
            LoadBookDetails();
            LoadAuthors();
        }

        // Заповнення полів даними про книгу
        private void LoadBookDetails()
        {
            TitleTextBox.Text = _bookToEdit.Title;
            GenreTextBox.Text = _bookToEdit.Genre;
            CreateBookDatePicker.SelectedDate = _bookToEdit.CreateBook;
            PriceTextBox.Text = _bookToEdit.Price.ToString();
            QuantityTextBox.Text = _bookToEdit.QuantityInStock.ToString();
            AuthorsComboBox.SelectedValue = _bookToEdit.AuthorId;
        }

        // Завантаження авторів у ComboBox
        private void LoadAuthors()
        {
            AuthorsComboBox.ItemsSource = _context.Authors.ToList();
            AuthorsComboBox.DisplayMemberPath = "Name";
            AuthorsComboBox.SelectedValuePath = "AuthorId";
        }

        // Збереження змін
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            // Оновлення властивостей книги
            _bookToEdit.Title = TitleTextBox.Text;
            _bookToEdit.Genre = GenreTextBox.Text;
            _bookToEdit.CreateBook = CreateBookDatePicker.SelectedDate ?? _bookToEdit.CreateBook;
            _bookToEdit.Price = decimal.Parse(PriceTextBox.Text);
            _bookToEdit.QuantityInStock = int.Parse(QuantityTextBox.Text);

            // Завантажуємо існуючого автора з контексту замість створення нового
            var authorId = (int)AuthorsComboBox.SelectedValue;
            var author = _context.Authors.SingleOrDefault(a => a.AuthorId == authorId);

            if (author != null)
            {
                _bookToEdit.AuthorName = author;
            }

            // Збереження змін у базі даних
            _context.Books.Update(_bookToEdit);
            _context.SaveChanges();

            MessageBox.Show("Зміни успішно збережено!");
            Close();
        }
    }
}
