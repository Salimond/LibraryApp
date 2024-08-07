using WebApplication1.Repository;
using WebApplication1.Models;


namespace LibraryTest
{
    [TestClass]
    public class BookTests
    {

        private IBookRepository _bookRepository;
        private UserRepository _userRepository;


        [TestInitialize]
        public void BookTestsSetup()
        {
            _bookRepository = new BookRepository();
            _userRepository = new UserRepository();
        }

        [TestMethod]
        public void AddBook_SuccessfulAdd()
        {
            _bookRepository.AddBook("name1", "name1", 123);
            Assert.IsTrue(_bookRepository.GetAllBooks().Count == 1);

            _bookRepository.AddBook("name2", "name2", 456);
            Assert.IsTrue(_bookRepository.GetAllBooks().Count == 2);
        }

        [TestMethod]
        public void AddBook_DuplicateAdd()
        {
            _bookRepository.AddBook("name1", "name1", 123);
            Assert.IsTrue(_bookRepository.GetAllBooks().Count == 1);

            try
            {
                _bookRepository.AddBook("name1", "name1", 123);
                Assert.Fail();
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]
        public void GetBookByISBN_Success()
        {
            _bookRepository.AddBook("name1", "name1", 123);
            var book1 = _bookRepository.GetBookByISBN(123);

            Assert.IsNotNull(book1);

            var book2 = _bookRepository.GetBookByISBN(321);
            Assert.IsNull(book2);
        }

        [TestMethod]
        public void TakeOutBook_Success()
        {
            _bookRepository.AddBook("name1", "name1", 123);
            _userRepository.AddUser("Username1");
            var userList = _userRepository.GetAllUsers();
            _userRepository.TakeOutBook(userList[0].UserId, 123);
            _bookRepository.TakeOutBook(123, userList[0].UserId);

            Book book = _bookRepository.GetBookByISBN(123);

            Assert.AreEqual(userList[0].BooksISBN[0], 123);
            Assert.IsFalse(book.Available);
            Assert.AreEqual(book.UserWhoHas, userList[0].UserId);
        }
    }
}