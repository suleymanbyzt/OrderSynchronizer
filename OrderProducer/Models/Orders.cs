using Bogus;

namespace OrderProducer.Models;

public class Orders
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string PairSymbol { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }

    public static Faker<Orders> FakeData { get; } =
        new Faker<Orders>()
            .RuleFor(o => o.Id, f => f.Hacker.Random.Long())
            .RuleFor(o => o.UserId, f => f.Random.Long())
            .RuleFor(o => o.PairSymbol, f => f.Finance.Currency().Code)
            .RuleFor(o => o.Amount, f => f.Finance.Amount())
            .RuleFor(o => o.Price, f => f.Finance.Amount());
}