using SharedKernel;

namespace Domain.SmsConfigs;

public sealed class SmsConfig : Entity
{
    public Guid Id { get; set; }
    public string SmsToken { get; set; } = string.Empty;
}
