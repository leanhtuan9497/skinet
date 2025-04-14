using System.Runtime.CompilerServices;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CartController(ICartService cartService) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<ShoppingCart>> GetCardById(string id)
    {
        var cart = await cartService.GetCartAsync(id);

        return Ok(cart ?? new ShoppingCart { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
    {
        var updatedCart = await cartService.SetCartAsync(cart);

        if (updatedCart == null)
        {
            return BadRequest("Problem with cart");
        }

        return updatedCart;
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteCart(string id)
    {
        var deleted = await cartService.DeleteCartAsync(id);

        if (!deleted)
        {
            return BadRequest("Problem deleting cart");
        }

        return deleted;
    }
}
