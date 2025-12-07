using SharedKernel;

namespace Domain.Countries;

public class Country : Entity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Capital { get; set; }
    public string PhoneCode { get; set; } = null!;
    public bool IsActive { get; set; }
}

