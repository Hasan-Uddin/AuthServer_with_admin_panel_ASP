using Domain.Roles;
using Domain.UserRoles;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.UserRoles;

internal class UserRoleConfig : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasIndex(x => x.UserId);

        builder.HasKey(x => x.Id);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Role>()
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.RoleId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();
    }
}
