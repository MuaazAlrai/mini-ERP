using Microsoft.AspNetCore.Mvc;
using MiniERP.DTOs;
using MiniERP.Models;
using MiniERP.Repositories;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IRepository<Product> _repo;

    public ProductsController(IRepository<Product> repo)
    {
        _repo = repo;
    }

    // ✅ GET: api/products
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _repo.GetAll());

    // ✅ POST: api/products
    [HttpPost]
    public async Task<IActionResult> Create(ProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock
        };

        await _repo.Add(product);
        return Ok(product);
    }

    // ✅ PUT: api/products/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductDto dto)
    {
        var product = await _repo.GetById(id);

        if (product == null)
            return NotFound("Product not found");

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.Stock = dto.Stock;

        await _repo.Update(product);

        return Ok(new
        {
            message = "Product updated successfully",
            product
        });
    }

    // ❌ DELETE: api/products/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _repo.GetById(id);

        if (product == null)
            return NotFound("Product not found");

        await _repo.Delete(product);

        return Ok(new
        {
            message = "Product deleted successfully"
        });
    }
}