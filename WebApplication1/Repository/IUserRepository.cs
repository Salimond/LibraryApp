using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IUserRepository
    {
        public List<User> GetAllUsers();
        public void AddUser(string username);
        public User GetUserByID(Guid id);
        public void DeleteUser(Guid id);
        public void TakeOutBook(Guid id, int isbn);
        public void ReturnBook(Guid id, int isbn);
    }
}
