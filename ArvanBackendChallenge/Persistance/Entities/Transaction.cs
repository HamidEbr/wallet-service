using ArvanBackendChallenge.Exceptions;
using ArvanBackendChallenge.Validators;

namespace ArvanBackendChallenge.Persistance.Entities;

public class Transaction : BaseEntity
{
    private Transaction()
    {
    }

    public static Transaction CreatePromoTransaction(Guid promoCode, string userPhone, decimal amount)
        => !userPhone.IsValidPhone() ? throw new InvalidPhoneException(userPhone) :
        new()
        {
            PromoCodeId = promoCode,
            Amount = amount,
            UserPhone = userPhone,
        };

    public static Transaction CreateTransaction(string userPhone, decimal amount)
        => !userPhone.IsValidPhone() ? throw new InvalidPhoneException(userPhone) :
        new()
        {
            Amount = amount,
            UserPhone = userPhone,
        };

    public string UserPhone { get; init; }
    public decimal Amount { get; init; }
    public Guid? PromoCodeId { get; init; }
    public virtual PromoCode PromoCode { get; init; }
}