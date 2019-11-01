using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using aspnetapp.Services;
using GrpcGreeter;

namespace aspnetapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GreeterService _service;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _service = new GreeterService();
        }

        /// <summary>
        /// A method to say hello
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /{name}
        ///
        /// </remarks>
        /// <param name="name"></param>
        [HttpGet("{name}", Name = "SayHello")]
        public IActionResult SayHello(string name)
        {
            var response = _service.SayHello(new HelloRequest {Name = name}, null);
            return Ok(response);
        }
    }
}
