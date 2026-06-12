using Microsoft.AspNetCore.Mvc;
using MiniERP.Models;
using MiniERP.DTOs;
using MiniERP.Repositories;

namespace MiniERP.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly IRepository<Customer> _repo;

    public CustomersController(IRepository<Customer> repo)
    {
        _repo = repo;
    }

    // ✅ GET: api/customers
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _repo.GetAll();
        return Ok(customers);
    }

    // ✅ GET: api/customers/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _repo.GetById(id);

        if (customer == null)
            return NotFound("Customer not found");

        return Ok(customer);
    }

    // ✅ POST: api/customers
    [HttpPost]
    public async Task<IActionResult> Create(CustomerDto dto)
    {
        if (dto == null)
            return BadRequest("Invalid data");

        var customer = new Customer
        {
            Name = dto.Name,
            Phone = dto.Phone
        };

        await _repo.Add(customer);

        return Ok(new
        {
            message = "Customer created successfully",
            customer
        });
    }

    // ✅ PUT: api/customers/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CustomerDto dto)
    {
        var customer = await _repo.GetById(id);

        if (customer == null)
            return NotFound("Customer not found");

        customer.Name = dto.Name;
        customer.Phone = dto.Phone;

        await _repo.Update(customer); // ✅ FIXED

        return Ok(new
        {
            message = "Customer updated successfully",
            customer
        });
    }

    // ❗ DELETE: api/customers/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var customer = await _repo.GetById(id);

        if (customer == null)
            return NotFound("Customer not found");

        await _repo.Delete(customer);

        return Ok(new
        {
            message = "Customer deleted successfully"
        });
    }
}