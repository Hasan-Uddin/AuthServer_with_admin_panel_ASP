using SharedKernel;

namespace Domain.PasswordResets;

public static class PasswordResetsErrors
{
<<<<<<< Updated upstream
    public static Error AlreadyCompleted(Guid PrId) => Error.Problem(
        "PasswordReset.AlreadyCompleted",
        $"The Password reset with Id = '{PrId}' is already completed.");

    public static Error NotFound(Guid PrId) => Error.NotFound(
        "PasswordReset.NotFound",
        $"The password reset with the Id = '{PrId}' was not found");
=======
    public static Error AlreadyCompleted(Guid Id) => Error.Problem(
        "Password Reset.AlreadyCompleted",
        $"The Password reset with Id = '{Id}' is already completed.");

    public static Error NotFound(Guid Id) => Error.NotFound(
        "Password Reset.NotFound",
        $"The password reset with the Id = '{Id}' was not found");
>>>>>>> Stashed changes
}
