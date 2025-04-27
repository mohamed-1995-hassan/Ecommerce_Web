using Ecommerce_Web.Data;
using Ecommerce_Web.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : BaseController
    {
        private readonly StoreContext _context;
        public BuggyController(StoreContext context)
        {
            _context = context;
        }
        [HttpGet("not-found")]
        public ActionResult GetNotFoundResult()
        {
            var thing = _context.Products.Find(42);
            if(thing == null)
                return NotFound(new ApiResponse(404));

            return Ok();
        }

        [HttpGet("server-error")]
        public ActionResult GetNotServerErrorResult()
        {
            var thing = _context.Products.FirstOrDefault(x => x.Id == 42);
            thing.ToString();

            return Ok();
        }

        [HttpGet("bad-request")]
        public ActionResult GetBadRequestResult()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("bad-request/{id}")]
        public ActionResult GetBadRequestResult(int id)
        {
            return BadRequest();
        }
    }
}
