using Application.Abstractions.Messaging;
using Application.Token.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Token;

public sealed class Create : IEndpoint
{
    public sealed class Request
    {
<<<<<<< Updated upstream
        public Guid UserId { get; set; }
        public Guid AppId { get; set; }
        public string Accesstoken { get; set; } //text
        public string Refreshtoken { get; set; } // text
        public DateTime IssuedAt { get; set; }
=======
        public Guid User_id { get; set; }
        public Guid App_id { get; set; }
        public string Access_token { get; set; } //text
        public string Refresh_token { get; set; } // text
        public DateTime Issued_at { get; set; }
>>>>>>> Stashed changes
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("Token", async (
            Request request,
            ICommandHandler<CreateTokenCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateTokenCommand
            {
<<<<<<< Updated upstream
                UserId = request.UserId,
                AppId = request.AppId,
                Accesstoken = request.Accesstoken,
                Refreshtoken = request.Refreshtoken,
                IssuedAt = request.IssuedAt
=======
                User_id = request.User_id,
                App_id = request.App_id,
                Access_token = request.Access_token,
                Refresh_token = request.Refresh_token,
                Issued_at = request.Issued_at
>>>>>>> Stashed changes
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Token)
        .RequireAuthorization();
    }
}
