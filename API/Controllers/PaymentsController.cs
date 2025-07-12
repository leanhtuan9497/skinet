using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PaymentsController(IPaymentService paymentService
    , IGenericRepository<DeliveryMethod> dmRepo) : BaseApiController
{
    [Authorize]
    [HttpPost("{cartId}")]
    public async Task<ActionResult<ShoppingCart>> CreateOrUpdatePaymentIntent(string cartId)
    {
        try
        {
            var cart = await paymentService.CreateOrUpdatePaymentIntent(cartId);
            if (cart == null) return BadRequest("Problem with your cart");
            return Ok(cart);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error creating or updating payment intent: {ex.Message}");
        }
    }

    [HttpGet("delivery-methods")]
    public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
    {
        try
        {
            return Ok(await dmRepo.ListAllAsync());
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving delivery methods: {ex.Message}");
        }
    }
}
