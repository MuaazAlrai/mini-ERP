using Microsoft.AspNetCore.Mvc;
using MiniERP.Data;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            totalProducts = _context.Products.Count(),
            totalCustomers = _context.Customers.Count(),
            totalOrders = _context.Orders.Count()
        });
    }
}