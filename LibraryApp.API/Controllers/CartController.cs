using Microsoft.AspNetCore.Mvc;
using LibraryApp.Data.Services;

namespace LibraryApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCartItemsAsync(int userId)
    {
        var items = await _cartService.GetCartItemsAsync(userId);
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> AddItemToCartAsync(int bookId, int userId)
    {
        await _cartService.AddItemToCartAsync(bookId, userId);
        return Ok();
    }

    [HttpPost]
    [Route("order")]
    public async Task<IActionResult> CreateOrderAsync(int userId)
    {
        var order = await _cartService.CreateOrderAsync(userId);
        if(order is null)
        {
            return NotFound("Cart is empty");
        }
        return Ok(order);
    }

    [HttpPost]
    [Route("freebooks")]
    public async Task<IActionResult> RequestFreeBookAsync(int bookId, int userId)
    {
        var request = await _cartService.RequestFreeBookAsync(bookId, userId);
        if(request is null)
        {
            return BadRequest("User cannot request more than 2 books");
        }
        return Ok(request);
    }

    [HttpPut]
    [Route("freebooks")]
    public async Task<IActionResult> ReturnFreeBookAsync(int bookId, int userId)
    {
        var request = await _cartService.ReturnFreeBookAsync(bookId, userId);
        if(request is null)
        {
            return NotFound();
        }
        return Ok(request);
    }
}
