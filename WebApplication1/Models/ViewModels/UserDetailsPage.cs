namespace WebApplication1.Models.ViewModels
{
    public class UserDetailsPage
    {
        public User user { get; set; }
        public List<Book> books = new List<Book>();
    }
}
