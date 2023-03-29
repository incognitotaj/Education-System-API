using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseApiController
    {
        public AuthController()
        {
        }

        [HttpGet("login")]
        public ActionResult Login()
        {
            return Ok();
        }
    }
}
