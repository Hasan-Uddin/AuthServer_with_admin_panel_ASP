using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel;

namespace Domain.SmtpConfigs;

public sealed record SmtpConfigCreatedDomainEvent(Guid SmtpId) : IDomainEvent;
