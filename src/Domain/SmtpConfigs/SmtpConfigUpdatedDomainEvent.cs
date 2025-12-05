using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel;

namespace Domain.SmtpConfigs;

public sealed record SmtpConfigUpdatedDomainEvent(Guid SmtpId) : IDomainEvent;
