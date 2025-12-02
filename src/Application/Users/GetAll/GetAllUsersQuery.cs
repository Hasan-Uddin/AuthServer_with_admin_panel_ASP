using System;
using System.Collections.Generic;
using System.Text;
using Application.Abstractions.Messaging;

namespace Application.Users.GetAll;

public sealed record GetAllUsersQuery(Guid Id) : IQuery<List<GetAllUsersQueryResponse>>;
