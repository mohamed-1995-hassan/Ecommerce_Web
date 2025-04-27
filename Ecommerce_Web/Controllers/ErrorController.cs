
using Ecommerce_Web.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Web.Controllers
{
    public class ErrorController : BaseController
    {
        [HttpGet]
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
