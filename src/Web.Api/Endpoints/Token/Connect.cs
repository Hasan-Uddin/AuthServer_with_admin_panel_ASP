using Application.Abstractions.Messaging;
using Application.Token.Connect;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Token;

internal sealed class Connect : IEndpoint
{
    public sealed record Request(string ClientId, string ClientSecret);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Token(Base.Connect), async (
            Request request,
            ICommandHandler<ConnectTokenCommand, ConnectTokenResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new ConnectTokenCommand(request.ClientId, request.ClientSecret);

            Result<ConnectTokenResponse> result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Token);
    }
}