using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using aspnetapp.Services;
using GrpcGreeter;
using Microsoft.AspNetCore.Http;

namespace aspnetapp.Controllers
{
    /// <summary>
    /// Home controller
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GreeterService _service;
        
        /// <summary>
        /// Home constructor
        /// </summary>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _service = new GreeterService();
        }

        /// <summary>
        /// A simple method to say hello
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /{name}
        ///
        /// </remarks>
        /// <param name="name"></param>
        [HttpGet("{name}", Name = "SayHello")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SayHello(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var response = await _service.SayHello(new HelloRequest {Name = name}, null);
            return Ok(response);
        }
    }
}
