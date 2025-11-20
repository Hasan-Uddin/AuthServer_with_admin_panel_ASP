using Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Roles;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .HasColumnName("id")
            .IsRequired();
        builder.Property(r => r.RoleName)
            .HasColumnName("role_name")
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(r => r.Description)
            .HasColumnName("description")
            .HasColumnType("text");
        builder.HasIndex(r => r.RoleName)
            .IsUnique();


    }
}
