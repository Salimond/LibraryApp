using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;

        public BookController(IBookRepository bookRepository, IUserRepository userRepository) 
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            BookViewPage bookViewPage = new BookViewPage();
            bookViewPage.Books = _bookRepository.GetAllBooks();
            
            return View(bookViewPage);
        }

        public IActionResult Details(int ISBN)
        {
            BookDetailsPage bookDetailsPage = new BookDetailsPage();
            bookDetailsPage.book = _bookRepository.GetBookByISBN(ISBN);

            if (bookDetailsPage.book.Available == false)
            {
                User user = _userRepository.GetUserByID(bookDetailsPage.book.UserWhoHas);
                bookDetailsPage.avialabilityText = "Borrowed by " + user.Username;
            }
            else
            {
                bookDetailsPage.avialabilityText = "Available";
            }

            return View(bookDetailsPage);
        }

        public IActionResult CreateBook(string Title, string Author, string ISBN)
        {
            bool validISBN = int.TryParse(ISBN, out var isbnInt);

            if (!validISBN)
            {
                //error handling
            }

            _bookRepository.AddBook(Title, Author, isbnInt);
            return RedirectToAction("Index", "Book");
        }

        public IActionResult ReturnBook(Guid id, int passedISBN)
        {
            _bookRepository.ReturnBook(passedISBN);
            _userRepository.ReturnBook(id, passedISBN);

            return RedirectToAction("Details", "Book", new { ISBN = passedISBN});
        }

        public IActionResult TakeOutBook(Guid UserId, int passedISBN)
        {
            _bookRepository.TakeOutBook(passedISBN, UserId);
            _userRepository.TakeOutBook(UserId, passedISBN);

            return RedirectToAction("Details", "Book", new { ISBN = passedISBN });
        }

        public IActionResult DeleteBook(string ISBN)
        {
            bool validISBN = int.TryParse(ISBN, out var isbnInt);

            if (!validISBN)
            {
                //error handling
            }
            _bookRepository.DeleteBook(isbnInt);
            return RedirectToAction("Index", "Book");
        }
    }
}
