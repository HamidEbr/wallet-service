namespace ArvanBackendChallenge.Persistance.Entities;

public class PromoCode : BaseEntity
{
    private PromoCode()
    {
    }

    public static PromoCode CreatePromoCode(string code, decimal amount, int remainingCount) => new()
    {
        Code = code,
        Amount = amount,
        RemainingCount = remainingCount
    };

    public required string Code { get; init; }
    public required decimal Amount { get; init; }
    public int RemainingCount { get; set; }
}
