using ArvanBackendChallenge.Dtos;
using ArvanBackendChallenge.Exceptions;
using ArvanBackendChallenge.Persistance;
using ArvanBackendChallenge.Persistance.Entities;
using ArvanBackendChallenge.Validators;
using Microsoft.EntityFrameworkCore;

namespace ArvanBackendChallenge.Services;

internal class WalletService(WalletDbContext dbContext) : IWalletService
{
    private readonly WalletDbContext _dbContext = dbContext;

    public async Task ChargeWalletWithPromo(Guid promoCodeId, string phone, decimal amount)
        => await _dbContext.Transactions.AddAsync(Transaction.CreatePromoTransaction(promoCodeId, phone, amount));

    public async Task<BalanceDto> GetUserBallance(string phoneNumber)
        => !phoneNumber.IsValidPhone() ? throw new InvalidPhoneException(phoneNumber) : 
        new(await _dbContext.Transactions.Where(t => t.UserPhone == phoneNumber).SumAsync(x => x.Amount));

    public async Task<IEnumerable<TransactionDto>> GetUserTransactions(string phoneNumber)
        => !phoneNumber.IsValidPhone() ? throw new InvalidPhoneException(phoneNumber) : 
            await _dbContext.Transactions.Where(t => t.UserPhone == phoneNumber)
            .Select(tr => new TransactionDto(tr.UserPhone, tr.Amount, tr.CreatedDate))
            .ToListAsync();
}
