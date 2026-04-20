namespace OutboxDemo;

public class Order
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = default!;
}