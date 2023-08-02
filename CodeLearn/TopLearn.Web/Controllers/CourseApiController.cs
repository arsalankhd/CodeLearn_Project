using System.Linq;
using System.Threading.Tasks;
using CodeLearn.DataLayer.Context;
using Microsoft.AspNetCore.Mvc;

namespace CodeLearn.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseApiController : ControllerBase
    {
        private CodeLearnContext _context;

        public CourseApiController(CodeLearnContext context)
        {
            _context = context;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var courseTitle = _context.Courses
                    .Where(c => c.CourseTitle.Contains(term))
                    .Select(c => c.CourseTitle)
                    .ToList();
                return Ok(courseTitle);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
