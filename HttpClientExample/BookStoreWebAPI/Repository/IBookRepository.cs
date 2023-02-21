using OtherWebAPI.Model;

namespace OtherWebAPI.Repository
{
    public interface IBookRepository
    {
        public IEnumerable<Book> GetBooks();        
    }
}
