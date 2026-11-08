using api.Data;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Migrations;
using api.models;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;


namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/Order")]
    public class OrderController:ControllerBase
    {

        private readonly ECommerceDbContext _context;
        private readonly IOrderRepository _orderRepo;


        public OrderController(ECommerceDbContext context, IOrderRepository orderRepo)
        {
            _context = context;
            _orderRepo = orderRepo;
            
        }
        [HttpGet]

        public async Task<IActionResult> GetOrder()
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var orders = await _orderRepo.GetOrdersAsync(userId);
            if(orders == null) return NotFound("No Order Has been found");
            return Ok(orders.Select((o) => o.toDtoFromDb()));


        }
        [HttpPost]

        public async Task<IActionResult> CreateOrder()
        {
           var userId = User.GetUserId();
            if (userId == null) return Unauthorized();
            var order = await _orderRepo.CreateOrderFromCartAsync(userId);
            if (order == null) return BadRequest("not enough stock");
    
            return Ok(order.toDtoFromDb());


        }

    }
}
