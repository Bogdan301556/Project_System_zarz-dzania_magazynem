public enum OperationType
{
    Incoming, 
    Outgoing  
}

public class Operation
{
    public int ProductId { get; set; }
    public OperationType Type { get; set; }
    public int Quantity { get; set; }
    public DateTime Date { get; set; }

    public Operation(int productId, OperationType type, int quantity)
    {
        ProductId = productId;
        Type = type;
        Quantity = quantity;
        Date = DateTime.Now;
    }
}
