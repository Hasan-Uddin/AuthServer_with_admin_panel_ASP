using Application.Abstractions.Messaging;
using Domain.Businesses;

namespace Application.Businesses.Update;

public sealed record UpdateBusinessCommand(
    Guid Id,
    string BusinessName,
    string IndustryType,
#pragma warning disable CA1054
    string LogoUrl,
#pragma warning restore CA1054
    Status Status
) : ICommand<Guid>;
