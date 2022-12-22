using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibraryApp.Data;

namespace LibraryApp.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CartController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddItemToCart(int id){

            var cla = HttpContext.User.Identity.Name;
            return Ok();
        }
    }
}
