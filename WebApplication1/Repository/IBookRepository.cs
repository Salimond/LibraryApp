using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IBookRepository
    {
        public List<Book> GetAllBooks();
        public void AddBook(string title, string author, int isbn);
        public Book GetBookByISBN(int isbn);
        public void DeleteBook(int isbn);
        public void ReturnBook(int isbn);
        public void TakeOutBook(int isbn, Guid userId);
    }
}
