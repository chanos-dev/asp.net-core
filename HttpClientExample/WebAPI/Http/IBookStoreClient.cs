using Refit;

namespace WebAPI.Http
{
    public interface IBookStoreClient
    {
        [Get("/api/BookStore")]
        Task<string> GetBookStoreAsync();
    }
}
