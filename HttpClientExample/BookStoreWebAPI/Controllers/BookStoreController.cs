using Microsoft.AspNetCore.Mvc;
using OtherWebAPI.Model;
using OtherWebAPI.Repository;

namespace OtherWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookStoreController : ControllerBase
    {
        private readonly ILogger<BookStoreController> _logger;
        private readonly IBookRepository _bookRepository;
        public BookStoreController(ILogger<BookStoreController> logger, IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get Books!");
            return Ok(_bookRepository.GetBooks());
        }
    }
}