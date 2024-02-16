namespace ArvanBackendChallenge.Exceptions;

public class InvalidPhoneException(string phone) : Exception($"Phone number #{phone} is invalid!")
{
}
