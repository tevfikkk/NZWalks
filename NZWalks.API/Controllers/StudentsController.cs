using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    // https://localhost:portnumber/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // GET: https://localhost:portnumber/api/students
        [HttpGet]
        public IActionResult GetAll()
        {
            string[] studentNames = ["Gokhru", "Neo", "Jane", "John", "Mark"];

            return Ok(studentNames);
        }
    }
}
