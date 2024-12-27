using BookShop.AdditionalWindows;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookShop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private BookstoreContext _context = new BookstoreContext();
    public MainWindow()
    {
        InitializeComponent();
        LoadBooks();
    }

    private void LoadBooks()
    {
        // Завантаження книг разом із авторами
        var books = _context.Books.Include(b => b.AuthorName).ToList();
        BooksDataGrid.ItemsSource = books;
    }

    private void AddBookWindow_Click(object sender, RoutedEventArgs e)
    {
        var addBookWindow = new AddBookWindow();
        addBookWindow.ShowDialog();
        LoadBooks(); // Оновлення списку книг після додавання /редагування
    }

    private void EditBookWindow_Click(object sender, RoutedEventArgs e)
    {
        if (BooksDataGrid.SelectedItem is Book selectedBook)
        {
            var editBookWindow = new EditBookWindow(selectedBook);
            editBookWindow.ShowDialog();
            LoadBooks();
        }
        else
        {
            MessageBox.Show("Виберіть книгу для редагування!");
        }
    }

    private void DeleteBook_Click(object sender, RoutedEventArgs e)
    {
        if (BooksDataGrid.SelectedItem is Book selectedBook)
        {
            using (var context = new BookstoreContext())
            {
                context.Books.Remove(selectedBook);
                context.SaveChanges();
            }
            LoadBooks(); // Оновлення списку після видалення
        }
        else
        {
            MessageBox.Show("Виберіть книгу для видалення!");
        }
    }

    private void SearchBook_Click(object sender, RoutedEventArgs e)
    {
        var searchText = SearchBox.Text.ToLower();

        using (var context = new BookstoreContext())
        {
            var filteredBooks = context.Books
                .Where(b => b.Title.ToLower().Contains(searchText) ||
                            b.Genre.ToLower().Contains(searchText) ||
                            b.AuthorName.Name.ToLower().Contains(searchText)||
                            b.Price.ToString().Contains(searchText)||
                            b.CreateBook.ToString().Contains(searchText))
                .Include(b => b.AuthorName)
                .ToList();

            BooksDataGrid.ItemsSource = filteredBooks;
        }
    }

    private void SortBooks_Click(object sender, RoutedEventArgs e)
    {
        using (var context = new BookstoreContext())
        {
            var selectedSort = (SortByComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var sortedBooks = context.Books.Include(b => b.AuthorName).AsQueryable();

            switch (selectedSort)
            {
                case "Автор":
                    sortedBooks = sortedBooks.OrderBy(b => b.AuthorName.Name);
                    break;
                case "Назва":
                    sortedBooks = sortedBooks.OrderBy(b => b.Title);
                    break;
                case "Жанр":
                    sortedBooks = sortedBooks.OrderBy(b => b.Genre);
                    break;
                case "Рік написання":
                    sortedBooks = sortedBooks.OrderBy(b => b.CreateBook);
                    break;
                case "Ціна":
                    sortedBooks = sortedBooks.OrderBy(b => b.Price);
                    break;
                case "Кількість в наявності":
                    sortedBooks = sortedBooks.OrderBy(b => b.QuantityInStock);
                    break;
            }

            BooksDataGrid.ItemsSource = sortedBooks.ToList();
        }
    }
    
    private void ShowBooksByAuthor(int authorId)
    {
        using (var context = new BookstoreContext())
        {
            var booksByAuthor = context.Books
                .Where(b => b.AuthorId == authorId)
                .Include(b => b.AuthorName)
                .ToList();

            BooksDataGrid.ItemsSource = booksByAuthor;
        }
    }

    private void AddAuthorWindow_Click(object sender, RoutedEventArgs e)
    {
        // Відкриваємо вікно додавання автора
        var addAuthorWindow = new AddAuthorWindow();
        addAuthorWindow.ShowDialog();
    }
}