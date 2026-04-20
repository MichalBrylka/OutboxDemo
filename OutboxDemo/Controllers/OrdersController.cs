namespace OutboxDemo.Controllers;

using MassTransit;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IPublishEndpoint _publishEndpoint;

    public OrdersController(AppDbContext db, IPublishEndpoint publishEndpoint)
    {
        _db = db;
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    public async Task<IActionResult> Create(string productName)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            ProductName = productName
        };

        _db.Orders.Add(order);

        // THIS goes into Outbox, not RabbitMQ directly
        await _publishEndpoint.Publish(new OrderCreated(order.Id, productName));

        await _db.SaveChangesAsync();

        return Ok(order.Id);
    }
}