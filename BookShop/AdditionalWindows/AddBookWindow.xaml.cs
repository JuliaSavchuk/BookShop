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
using Microsoft.EntityFrameworkCore;

namespace BookShop.AdditionalWindows
{
    /// <summary>
    /// Логика взаимодействия для AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        private BookstoreContext _context = new BookstoreContext();

        public AddBookWindow()
        {
            InitializeComponent();
            LoadAuthors();
        }

        private void LoadAuthors()
        {
            AuthorsComboBox.ItemsSource = _context.Authors.ToList();
            AuthorsComboBox.DisplayMemberPath = "Name";
            AuthorsComboBox.SelectedValuePath = "AuthorId";
        }

        private void SaveBook_Click(object sender, RoutedEventArgs e)
        {

            var newBook = new Book
            {
                Title = TitleTextBox.Text,
                Genre = GenreTextBox.Text,
                CreateBook = CreateBookDatePicker.SelectedDate ?? DateTime.Now,
                Price = decimal.Parse(PriceTextBox.Text),
                QuantityInStock = int.Parse(QuantityTextBox.Text),
                AuthorId = (int)AuthorsComboBox.SelectedValue
            };

            _context.Books.Add(newBook);
            _context.SaveChanges();

            MessageBox.Show("Книга успішно додана!");
            Close();
        }
    }
}
