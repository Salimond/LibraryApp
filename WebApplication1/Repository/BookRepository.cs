using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class BookRepository : IBookRepository
    {
        private static List<Book> _books;

        public BookRepository()
        {
            _books = new List<Book>();
        }

        public List<Book> GetAllBooks()
        {
            return _books;
        }

        public Book GetBookByISBN(int isbn)
        {
            return _books.Where(_ => _.ISBN == isbn).FirstOrDefault();
        }

        public void AddBook(string title, string author, int isbn)
        {
            if (GetBookByISBN(isbn) != null)
            {
                throw new Exception("ISBN is already in use");
            }
            Book book = new Book(title, author, isbn);
            _books.Add(book);
        }

        public void DeleteBook(int isbn)
        {
            Book book = GetBookByISBN(isbn);
            if (book == null)
            {
                throw new Exception("Unable to find book with given ISBN");
            }
            _books.Remove(book);
        }

        public void TakeOutBook(int isbn, Guid userId)
        {
            Book book = GetBookByISBN(isbn);

            if (book == null)
            {
                throw new Exception("Unable to find book with given ISBN");
            }
            
            if (book.Available == false || book.UserWhoHas != Guid.Empty)
            {
                throw new Exception("Book is already out");
            }

            book.UserWhoHas = userId;
            book.Available = false;

        }

        public void ReturnBook(int isbn)
        {
            Book book = GetBookByISBN(isbn);
            if (book == null)
            {
                throw new Exception("Unable to find book with given ISBN");
            }

            if (book.Available == true || book.UserWhoHas == Guid.Empty)
            {
                throw new Exception("Book has already been returned");
            }
            book.UserWhoHas = Guid.Empty;
            book.Available = true;
        }

    }
}
