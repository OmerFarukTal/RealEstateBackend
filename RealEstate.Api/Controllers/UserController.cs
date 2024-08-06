using Microsoft.AspNetCore.Mvc;
using RealEstate.Api.Context;

namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RealEstateContext context;

        public UserController(RealEstateContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login()
        {
            return Ok();
        }

  
        [HttpPost]
        [Route("sign")]
        public IActionResult SignUp()
        { 
            return Ok(); 
        }

    }
}
