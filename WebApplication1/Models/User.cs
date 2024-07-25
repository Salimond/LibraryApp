namespace WebApplication1.Models
{
    public class User
    {
        public User(string userName) 
        {
            UserId = Guid.NewGuid();
            Username = userName;
            BooksISBN = new List<int>();
        }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public List<int> BooksISBN { get; set; }
    }
}
