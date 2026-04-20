namespace OutboxDemo;

public record OrderCreated(Guid OrderId, string ProductName);