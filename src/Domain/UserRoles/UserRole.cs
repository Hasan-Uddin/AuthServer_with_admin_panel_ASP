using SharedKernel;

namespace Domain.UserRoles;

public sealed class UserRole: Entity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
