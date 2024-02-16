using ArvanBackendChallenge.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArvanBackendChallenge.Persistance;

public class WalletDbContext : DbContext
{
    public WalletDbContext(DbContextOptions<WalletDbContext> options)
            : base(options)
    { }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<PromoCode> PromoCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PromoCode>()
            .HasKey(x => x.Id);

        base.OnModelCreating(modelBuilder);
    }
}