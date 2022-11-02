using api.Model;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{ 
    private readonly ILogger<PersonController> _logger;
    private readonly IPersonRepository _personRepository;

    public PersonController(ILogger<PersonController> logger,
        IPersonRepository personRepository)
    {
        _logger = logger;
        _personRepository = personRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Person>> Get()
    {
        return Ok(this._personRepository.GetAll());
    }
    
    [HttpGet("{id}")]
    public ActionResult<Person> Get(int id)
    {
        return Ok(this._personRepository.Get(id));
    }

    [HttpGet("sorting")]
    public ActionResult<Person> Get([FromQuery(Name = "sort")] bool sort)
    {
        if (sort)
            return Ok(this._personRepository.GetAll().OrderBy(p => p.Id));
        else 
            return Ok(this._personRepository.GetAll().OrderByDescending(p => p.Id));
    }

    [HttpPost]
    // response
    [Produces("application/json")]
    // request
    [Consumes("multipart/form-data", "application/json")] 
    public ActionResult<Person> Post([FromBody] Person person)
    { 
        return Ok(this._personRepository.Add(person));
    }
}