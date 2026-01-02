
using Domain.Countries;
using Domain.Localities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Seed;

internal static class RegionsSeed
{
    internal static void Apply(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>().HasData(Contries());
        modelBuilder.Entity<Division>().HasData(Users());
        modelBuilder.Entity<Locality>().HasData(UserRoles());
    }

    private static Country[] Contries() =>
    [
        new Country
        {
            Id = Guid.Parse("b2a16d2f-4512-452b-abfe-baa28ea9da59"),
            Name = "Bangladesh",
            Capital = "Dhaka",
            PhoneCode = "+880",
            IsActive = true
        }
    ];
}
