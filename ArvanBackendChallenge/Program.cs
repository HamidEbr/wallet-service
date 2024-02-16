using ArvanBackendChallenge.Persistance;
using ArvanBackendChallenge.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<IChargeService, ChargeService>();
builder.Services.AddDbContext<WalletDbContext>(
        options => options.UseSqlite(builder.Configuration.GetConnectionString("WalletDb"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region Wallet Apis

app.MapGet("/api/v1/wallets/{phoneNumber}/transactions", (IWalletService walletService, string phoneNumber)
    => walletService.GetUserTransactions(phoneNumber))
.WithName("GetUserTransactions")
.WithOpenApi()
.WithTags("Wallets");

app.MapGet("/api/v1/wallets/{phoneNumber}/balance", (IWalletService walletService, string phoneNumber)
    => walletService.GetUserBallance(phoneNumber))
.WithName("GetUserBalance")
.WithOpenApi()
.WithTags("Wallets");

app.MapPost("/api/v1/wallets/promo-charge", (IChargeService chargeService, string phoneNumber, string promoCode)
    => chargeService.ChargeWalletUsingPromo(phoneNumber, promoCode))
.WithName("ChargeWalletWithPromo")
.WithOpenApi()
.WithTags("Wallets");

#endregion

#region Admin Apis

app.MapGet("/api/v1/admin/promo-charges", (IChargeService chargeService, string code) 
    => chargeService.GetPromoCharges(code))
.WithName("GetPromoUsageReport")
.WithOpenApi()
.WithTags("Admin");


app.MapPost("/api/v1/admin/promo-codes", (IChargeService chargeService, string code, decimal amount, int count)
    => chargeService.AddPromoCode(code, amount, count))
.WithName("AddPromoCode")
.WithOpenApi()
.WithTags("Admin");

#endregion

app.Run();