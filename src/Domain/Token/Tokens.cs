using SharedKernel;

namespace Domain.Token;

public class Tokens : Entity
{
<<<<<<< Updated upstream
    public Guid TokenId { get; set; }
    public Guid UserId { get; set; }
    public Guid AppId { get; set; }
    public string Accesstoken { get; set; } = string.Empty; //text
    public string Refreshtoken { get; set; } = string.Empty; // text
    public DateTime IssuedAt { get; set; }
=======
    public Guid Id { get; set; }
    public Guid User_id { get; set; }
    public Guid App_id { get; set; }
    public string Access_token { get; set; } //text
    public string Refresh_token { get; set; } // text
    public DateTime Issued_at { get; set; }
>>>>>>> Stashed changes
}
