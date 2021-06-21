using System.Linq;
using System.Threading.Tasks;
using aspnetapp.Entities;
using aspnetapp.Services;
using Greet.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace aspnetapp.Controllers
{
    /// <summary>
    /// Hello controller
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class HelloController : Controller
    {
        private readonly ILogger<HelloController> _logger;
        private readonly IGreeterService _service;
        
        /// <summary>
        /// Hello constructor
        /// </summary>
        public HelloController(ILogger<HelloController> logger, IGreeterService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// A simple method to say hello
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /hello/{name}
        ///
        /// </remarks>
        /// <param name="name"></param>
        [HttpGet("message/{name}", Name = "SayHello")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HelloReply))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SayHello([FromRoute] string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            
            // Forward the call to the greeter service
            var response = await _service.SayHello(new HelloRequest {Name = name}, null);
            return Ok(response);
        }

        [HttpPost("books", Name = "SayHelloPost")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HelloReply))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostName([FromBody] Entities.BookDTO book)
        {
            if (book == null)
                return BadRequest();

          
            return Ok(book);
        }

        [HttpPost("bookslist", Name = "BooksListPost")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HelloReply))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> PostBooks([FromBody] Entities.BookListDTO books)
        {
            if (books == null)
                return BadRequest();

           
            return Ok(books.books.Count());
        }
    }
}
