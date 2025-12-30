using Application.Abstractions.Data;
using Domain.Areas;
using Domain.AuditLogs;
using Domain.Countries;
using Domain.Districts;
using Domain.EmailVerification;
using Domain.Localities;
using Domain.Otps;
using Domain.Permissions;
using Domain.Regions;
using Domain.RolePermissions;
using Domain.Roles;
using Domain.SmsConfigs;
using Domain.SmtpConfigs;
using Domain.Todos;
using Domain.UserRoles;
using Domain.Users;
using Infrastructure.DomainEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SharedKernel;

namespace Infrastructure.Database;

public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IDomainEventsDispatcher domainEventsDispatcher)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<EmailVerifications> EmailVerifications { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Otp> Otp { get; set; }
    public DbSet<SmtpConfig> SmtpConfig { get; set; }
    public DbSet<SmsConfig> SmsConfig { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Area> Areas { get; set; }
    public DbSet<Locality> Localities { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public new EntityEntry Entry(object entity) => base.Entry(entity);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.HasDefaultSchema(Schemas.Default);

        //var passwordHasher = new PasswordHasher();
        //Console.WriteLine(passwordHasher.Hash("password")); // for admin
        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                RoleName = "Administrator",
                RoleCode = RoleCode.Admin,
                Description = "System Administrator with full access"
            },
            new Role
            {
                Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                RoleName = "Support",
                RoleCode = RoleCode.Support,
                Description = "Support Engineers"
            },
            new Role
            {
                Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                RoleName = "Analytics",
                RoleCode = RoleCode.Analytics,
                Description = "Helps in Analysis"
            },
            new Role
            {
                Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                RoleName = "PaymentAdmin",
                RoleCode = RoleCode.PaymentAdmin,
                Description = "Asses the payments"
            },
            new Role
            {
                Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                RoleName = "Common Usser",
                RoleCode = RoleCode.NormalUser,
                Description = "Common/Normal User"
            }
        );
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                Email = "admin@authserver.com",
                FullName = "Default ADMIN",
                PasswordHash = "55EF2DAE94973ADC24603DFB738E8D9F3B0B79D78AE8E111996F9C9F6AEDF9B4-604A0533DEFFDC0841B9DB1D6DF007E6",
                CreatedAt = new DateTime(2025, 12, 16, 0, 0, 0, DateTimeKind.Utc)
            }
        );
        modelBuilder.Entity<UserRole>().HasData(
            new UserRole
            {
                Id = Guid.Parse("aaaaaaaa-eeee-ffff-ffff-ffffffffffff"),
                UserId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                RoleId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            }
        );
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {


        int result = await base.SaveChangesAsync(cancellationToken);

        await PublishDomainEventsAsync();

        return result;
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                List<IDomainEvent> domainEvents = entity.DomainEvents;

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        await domainEventsDispatcher.DispatchAsync(domainEvents);
    }
}
