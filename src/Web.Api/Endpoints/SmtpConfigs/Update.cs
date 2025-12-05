using Application.Abstractions.Messaging;
using Application.SmtpConfigs.SmtpUpdate;
using Domain.SmtpConfigs;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SmtpConfigs;

internal sealed class Update : IEndpoint
{
    public sealed class Request
    {
        public Guid SmtpId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SenderEmail { get; set; }

    }
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.SmtpConfig.Update, async (
            Guid id,
            Request request,
            ICommandHandler<UpdateSmtpCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateSmtpCommand(
               request.SmtpId,
               request.Username,
               request.Password,
               request.SenderEmail
            );

            Result result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.NoContent, CustomResults.Problem);
        })
            .WithTags(Tags.Users)
            .RequireAuthorization();
    }
}
