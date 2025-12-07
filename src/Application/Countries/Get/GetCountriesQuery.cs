using Application.Abstractions.Messaging;
using Application.Countries.Get;

namespace Application.Countries.Get;

public sealed record GetCountriesQuery() : IQuery<List<GetCountryResponse>>;
