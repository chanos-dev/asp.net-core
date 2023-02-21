using OtherWebAPI.Model;

namespace OtherWebAPI.Repository
{    
    public class BookRepository : IBookRepository
    {
        public static List<Book> MemoryDatas { get; set; }

        static BookRepository()
        {
            MemoryDatas = new List<Book>();
            MemoryDatas.AddRange(Enumerable.Range(1, 10).Select(i => new Book()
            {
                Title = $"책{i}",
                Description = $"설명{i}",
                Author = $"작가{i}",
            })); 
        }
        public IEnumerable<Book> GetBooks() => MemoryDatas;
    }
}
