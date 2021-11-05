using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {

        }

        [HttpPost]
        public IActionResult Test()
        {
            System.Console.WriteLine("It Works!!!");

            return Ok("It is Okay");
        }
    }
}
