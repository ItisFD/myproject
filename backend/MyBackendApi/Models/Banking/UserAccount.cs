

using MyBackendApi.Helpers;

namespace MyBackendApi.Models.Banking
{
    public class UserAccount : BaseDapperModel
    {
    public int Id { get; set; }
    public int UserId { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string CurrencyCode { get; set; } = "USD";
    public string? Nickname { get; set; }
    public bool IsArchived { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    }
}