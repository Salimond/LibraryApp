using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;

        public UserController(IBookRepository bookRepository, IUserRepository userRepository)
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            UserViewPage model = new UserViewPage();
            model.Users = _userRepository.GetAllUsers();

            return View(model);
        }

        public IActionResult Details(string id)
        {
            User user = _userRepository.GetUserByID(Guid.Parse(id));
            List<Book> books = _bookRepository.GetAllBooks().Where(_ => _.UserWhoHas == Guid.Parse(id)).ToList();
            UserDetailsPage model = new UserDetailsPage();
            model.books = books;
            model.user = user;
            return View(model);
        }


        public IActionResult CreateUser(string Username)
        {
            _userRepository.AddUser(Username);
            return RedirectToAction("Index", "User");
        }

        public IActionResult DeleteUser(string id)
        {
            Guid.TryParse(id, out var userId);
            _userRepository.DeleteUser(userId);
            return RedirectToAction("Index", "User");
        }
    }
}
