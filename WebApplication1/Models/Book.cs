namespace WebApplication1.Models
{
    public class Book
    {
        public Book (string title, string author, int iSBN)
        {
            Title = title;
            Author = author;
            ISBN = iSBN;
            Available = true;
            UserWhoHas = Guid.Empty;
        }

        public string Title { get; set; }
        public string Author { get; set; } 
        public int ISBN { get; set; }
        public bool Available { get; set; }
        public Guid UserWhoHas { get; set; }
    }
}
