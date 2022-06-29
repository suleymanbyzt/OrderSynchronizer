namespace OrderConsumer.Models;

public class Orders
{
    public long Id { get; set; }
    
    public long UserId { get; set; }
    
    public string PairSymbol { get; set; }
    
    public decimal Amount { get; set; }
    
    public decimal Price { get; set; }
}