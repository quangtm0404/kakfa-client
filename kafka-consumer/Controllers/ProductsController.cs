using kafka_consumer.Datas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kafka_consumer.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext dbContext;
    public ProductsController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        return Ok(await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id));
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await dbContext.Products.ToListAsync();
        return result?.Count > 0
            ? Ok(result)
            : BadRequest();
    }
}