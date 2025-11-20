using SharedKernel;

namespace Domain.Token;

public static class TokenErrors
{
<<<<<<< Updated upstream
    public static Error AlreadyCompleted(Guid TokenId) => Error.Problem(
        "Token.AlreadyCompleted",
        $"The token with Id = '{TokenId}' is already completed.");

    public static Error NotFound(Guid TokenId) => Error.NotFound(
        "Tokens.NotFound",
        $"The token with the Id = '{TokenId}' was not found");
=======
    public static Error AlreadyCompleted(Guid Id) => Error.Problem(
        "Token.AlreadyCompleted",
        $"The token with Id = '{Id}' is already completed.");

    public static Error NotFound(Guid Id) => Error.NotFound(
        "Tokens.NotFound",
        $"The token with the Id = '{Id}' was not found");
>>>>>>> Stashed changes
}
