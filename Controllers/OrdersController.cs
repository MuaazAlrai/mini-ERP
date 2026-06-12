using Microsoft.AspNetCore.Mvc;
using MiniERP.DTOs;
using MiniERP.Models;
using MiniERP.Repositories;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IRepository<Order> _repo;

    public OrdersController(IRepository<Order> repo)
    {
        _repo = repo;
    }

    // GET ALL
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _repo.GetAll());

    // GET BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _repo.GetById(id);

        if (order == null)
            return NotFound();

        return Ok(order);
    }

    // CREATE
    [HttpPost]
    public async Task<IActionResult> Create(OrderDto dto)
    {
        var order = new Order
        {
            CustomerId = dto.CustomerId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            OrderDate = DateTime.UtcNow
        };

        await _repo.Add(order);
        return Ok(order);
    }

    // UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, OrderDto dto)
    {
        var order = await _repo.GetById(id);

        if (order == null)
            return NotFound();

        order.CustomerId = dto.CustomerId;
        order.ProductId = dto.ProductId;
        order.Quantity = dto.Quantity;

        await _repo.Update(order);

        return Ok(order);
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _repo.GetById(id);

        if (order == null)
            return NotFound();

        await _repo.Delete(order);

        return Ok(new { message = "Order deleted successfully" });
    }
}