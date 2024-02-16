using ArvanBackendChallenge.Dtos;
using ArvanBackendChallenge.Persistance.Entities;

namespace ArvanBackendChallenge.Services;

public interface IWalletService
{
    Task ChargeWalletWithPromo(Guid promoCodeId, string phone, decimal amount);
    Task<BalanceDto> GetUserBallance(string phoneNumber);
    Task<IEnumerable<TransactionDto>> GetUserTransactions(string phoneNumber);
}
