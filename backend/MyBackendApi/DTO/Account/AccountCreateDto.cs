namespace MyBackendApi.DTO.Account
{
    public class AccountCreateDto
    {
        public int UserId { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string CurrencyCode { get; set; } = "USD";
        public string? Nickname { get; set; }
        public bool IsArchived { get; set; } = false;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

    }
}