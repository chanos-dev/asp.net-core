using Domain;
using Microsoft.AspNetCore.Mvc;

namespace DockerExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private static readonly List<Book> _inMemoryBooks;

        private readonly ILogger<BookController> _logger;

        static BookController()
        {
            _inMemoryBooks = new List<Book>()
            {
                new Book()
                {
                    ISBN = "123-45-67890-12-3",
                    Title = "First Book",
                    Description = "Fisrt",
                    Author = "BaekFirst",
                },
                new Book()
                {
                    ISBN = "098-76-54321-09-8",
                    Title = "Second Book",
                    Description = "Second",
                    Author = "BaekSecond",
                },
            };
        }

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger; 
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_inMemoryBooks);
        }

        [HttpGet("{isbn}")]
        public IActionResult Get(string isbn)
        {
            return Ok(_inMemoryBooks.FirstOrDefault(book => book.ISBN == isbn));
        }
    }
}