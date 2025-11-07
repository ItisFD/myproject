using MyBackendApi.Helpers;

namespace MyBackendApi.Models.Banking
{
    public enum AccountTransactionType
    {
        DEPOSIT,
        WITHDRAWAL,
        TRANSFER,
        ADJUSTMENT
    }
    public enum StatusType
    {
        PENDING,
        COMPLETED,
        FAILED,
        REVERSED
    }
    public class Transaction : BaseDapperModel
    {
        public int Id { get; set; }
        public int FromAccountId { get; set; }
        public int? ToAccountId { get; set; } // may not have dest account in system - deposits, withdrawals
        public decimal Amount { get; set; }
        public string? CurrencyCode { get; set; } = "USD";
        public AccountTransactionType TransactionType { get; set; }
        public string? Description { get; set; }
        public StatusType Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}