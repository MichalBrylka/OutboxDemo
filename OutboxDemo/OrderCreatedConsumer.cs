using MassTransit;

namespace OutboxDemo;
public class OrderCreatedConsumer : IConsumer<OrderCreated>
{
    public Task Consume(ConsumeContext<OrderCreated> context)
    {
        Console.WriteLine($"Order created: {context.Message.OrderId}");

        return Task.CompletedTask;
    }
}