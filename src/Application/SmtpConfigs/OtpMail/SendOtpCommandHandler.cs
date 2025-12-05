using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Otps.Create;
using Domain.SmtpConfigs;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using SharedKernel;

namespace Application.SmtpConfigs.OtpMail;

internal sealed class SendOtpCommandHandler(
    IApplicationDbContext context,
    ICommandHandler<CreateOtpCommand, Guid> handler)
    : ICommandHandler<SendOtpCommand, Guid>
{
    public async Task<Result<Guid>> Handle(SendOtpCommand command, CancellationToken cancellationToken)
    {
        SmtpConfig? smtpConfig = await context.SmtpConfig.FirstOrDefaultAsync(cancellationToken);

        if (smtpConfig is null)
        {
            return Result.Failure<Guid>("SMTP configuration not found.");
        }

        var createOtpCommand = new CreateOtpCommand(command.RecipientEmail);
        Result<Guid> otpResult = await handler.Handle(createOtpCommand, cancellationToken);

        if (otpResult.IsFailure)
        {
            return Result.Failure<Guid>("OTP generation failed.");
        }

        Guid otp = otpResult.Value;

        using var message = new MimeMessage();
        message.From.Add(new MailboxAddress(smtpConfig.SenderEmail, smtpConfig.SenderEmail));
        message.To.Add(new MailboxAddress(command.RecipientEmail, command.RecipientEmail));
        message.Subject = "OTP Verification Mail";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $"Your OTP is: {otp}"
        };
        message.Body = bodyBuilder.ToMessageBody();

        try
        {
            using var client = new SmtpClient();

            SecureSocketOptions sslOption = smtpConfig.EnableSsl
                ? SecureSocketOptions.StartTls
                : SecureSocketOptions.None;

            await client.ConnectAsync(
                smtpConfig.Host,
                smtpConfig.Port,
                sslOption,
                cancellationToken);

            await client.AuthenticateAsync(
                smtpConfig.Username,
                smtpConfig.Password,
                cancellationToken);

            await client.SendAsync(message, cancellationToken);

            await client.DisconnectAsync(true, cancellationToken);

            return Result.Success(otp);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>($"Email sending failed: {ex.Message}");
        }
    }
}
