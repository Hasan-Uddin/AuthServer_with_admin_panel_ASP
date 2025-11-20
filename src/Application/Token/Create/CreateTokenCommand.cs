using Application.Abstractions.Messaging;

namespace Application.Token.Create;

public sealed class CreateTokenCommand : ICommand<Guid>
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
