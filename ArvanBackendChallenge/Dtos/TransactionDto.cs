namespace ArvanBackendChallenge.Dtos;

public sealed record TransactionDto(string UserPhone, decimal Amount, DateTime TransactionDate);