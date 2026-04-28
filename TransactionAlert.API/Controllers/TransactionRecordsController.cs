using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionAlert.Shared;

namespace TransactionAlert.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionRecordsController : ControllerBase
{
    private readonly TransactionDbContext _db;

    public TransactionRecordsController(TransactionDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var records = await _db.Transactions
            .OrderByDescending(t => t.Timestamp)
            .ToListAsync();
        return Ok(records);
    }
}