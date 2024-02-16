using ArvanBackendChallenge.Dtos;
using ArvanBackendChallenge.Persistance;
using ArvanBackendChallenge.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArvanBackendChallenge.Services;

internal class ChargeService(WalletDbContext dbContext, IWalletService walletService) : IChargeService
{
    private readonly IWalletService _walletService = walletService;
    private readonly WalletDbContext _dbContext = dbContext;

    public Task AddPromoCode(string code, decimal amount, int count)
    {
        _dbContext.PromoCodes.Add(PromoCode.CreatePromoCode(code, amount, count));
        return _dbContext.SaveChangesAsync();
    }

    public Task ChargeWalletUsingPromo(string phone, string promoCode)
    {
        using var dbTransaction = _dbContext.Database.BeginTransaction();
        var promo = _dbContext.PromoCodes.FirstOrDefault(p => p.Code == promoCode);

        if (promo == null || promo.RemainingCount <= 0)
        {
            return dbTransaction.RollbackAsync();
        }

        // Atomic check and update
        _dbContext.Entry(promo).Property(x => x.RemainingCount).CurrentValue -= 1;

        try
        {
            _dbContext.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Handle race condition
            return dbTransaction.RollbackAsync();
        }

        // Charge wallet
        _walletService.ChargeWalletWithPromo(promo.Id, phone, promo.Amount);

        return dbTransaction.CommitAsync();
    }

    public async Task<IEnumerable<TransactionDto>> GetPromoCharges(string promoCode)
        => await _dbContext.Transactions.Where(t => t.PromoCode.Code == promoCode)
        .Select(tr => new TransactionDto(tr.UserPhone, tr.Amount, tr.CreatedDate)).ToListAsync();
}
