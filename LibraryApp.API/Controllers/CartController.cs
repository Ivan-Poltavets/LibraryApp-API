using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibraryApp.Data.Services;

namespace LibraryApp.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost]
    public async Task<IActionResult> AddItemToCartAsync(int id)
    {
        var userId = int.Parse(HttpContext.User.Claims.ElementAt(0).Value);
        await _cartService.AddItemToCartAsync(id, userId);
        return Ok();
    }
}
