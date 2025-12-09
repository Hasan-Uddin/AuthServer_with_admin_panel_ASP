using Application.Abstractions.Messaging;
using Application.Areas.Get;

namespace Application.Areas.GetAll;

public sealed record GetAllAreasQuery() : IQuery<List<AreaResponse>>;
