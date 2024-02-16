using ArvanBackendChallenge.Dtos;
using ArvanBackendChallenge.Persistance.Entities;

namespace ArvanBackendChallenge.Services;

public interface IChargeService
{
    Task ChargeWalletUsingPromo(string phone, string promoCode);
    Task<IEnumerable<TransactionDto>> GetPromoCharges(string promoCode);
    Task AddPromoCode(string code, decimal amount, int count);
}
