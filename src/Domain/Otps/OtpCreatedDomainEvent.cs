using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel;

namespace Domain.Otps;

public sealed record OtpCreatedDomainEvent(Guid OtpId) : IDomainEvent;
