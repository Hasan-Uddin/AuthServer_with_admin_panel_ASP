using SharedKernel;

namespace Domain.EmailVerification;

public static class EmailVerificationErrors
{
<<<<<<< Updated upstream
    public static Error AlreadyCompleted(Guid EvId) => Error.Problem(
        "Email verification.AlreadyCompleted",
        $"The Email Verification with Id = '{EvId}' is already completed.");

    public static Error NotFound(Guid EvId) => Error.NotFound(
        "Email verification",
        $"The Email Verification with the Id = '{EvId}' was not found");
=======
    public static Error AlreadyCompleted(Guid Id) => Error.Problem(
        "Email verification.AlreadyCompleted",
        $"The Email Verification with Id = '{Id}' is already completed.");

    public static Error NotFound(Guid Id) => Error.NotFound(
        "Email verification",
        $"The Email Verification with the Id = '{Id}' was not found");
>>>>>>> Stashed changes
}
