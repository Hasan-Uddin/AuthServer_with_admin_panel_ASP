using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Businesses.Get;

public sealed record GetBusinessesQuery(Guid OwnerUserId) : IQuery<List<BusinessResponse>>;
