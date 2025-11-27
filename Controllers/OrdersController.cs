using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ordem_servico_backend.Models;
using ordem_servico_backend.Repositories.DatabaseContext;
using ordem_servico_backend.Repositories.Interface;
using ordem_servico_backend.Services.Implementation;
using ordem_servico_backend.Services.Interface;
using ordem_servico_backend.Utils;

namespace ordem_servico_backend.Controllers
{
    [Authorize(Roles = "admin,tecnico")]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepo;

        public OrdersController(IOrderService orderService) => _orderService = orderService;

        [HttpGet]
        
        public async Task<ActionResult<object>> GetOrders(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? search = null)
        {
            var (items, total) = await _orderService.GetPagedAsync(page, pageSize, search);
            return Ok(new { items, total });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderOutputDto>> GetOrderById(int id)
        {
            var dto = await _orderService.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
        {
            var created = await _orderService.CreateAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = created.Id }, created);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<OrderOutputDto>> UpdateOrder(int id, [FromBody] OrderUpdateDto dto)
        {
            var updated = await _orderService.UpdateChecklistAndPhotosAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var ok = await _orderService.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}