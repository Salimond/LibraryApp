using WebApplication1.Models;

namespace WebApplication1.Repository
{

    public class UserRepository : IUserRepository
    {
        private static List<User> _users;

        public UserRepository()
        {
            _users = new List<User>();
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public User GetUserByID(Guid id)
        {
            return _users.Where(_ => _.UserId == id).FirstOrDefault();
        }

        public void TakeOutBook(Guid id, int isbn)
        {
            User user = GetUserByID(id);

            if (user == null)
            {
                throw new Exception("No user found for that ID");
            }

            user.BooksISBN.Add(isbn);
        }

        public void ReturnBook(Guid id, int isbn)
        {
            User user = GetUserByID(id);

            if (user == null)
            {
                throw new Exception("No user found for that ID");
            }

            if (!user.BooksISBN.Contains(isbn))
            {
                throw new Exception("User does not have book currently");
            }

            user.BooksISBN.Remove(isbn);
        }
            

        public void AddUser(string username)
        {
            User user = new User(username);
            _users.Add(user);
        }


        public void DeleteUser(Guid id)
        {
            User user = GetUserByID(id);

            if (user == null)
            {
                throw new Exception("No user found for that ID");
            }

            _users.Remove(user);
        }

    }
}
