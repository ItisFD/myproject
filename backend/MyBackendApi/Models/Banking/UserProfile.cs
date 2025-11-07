
using MyBackendApi.Helpers;

namespace MyBackendApi.Models.Banking
{
    public enum UserRole
    {
        USER,
        ADMIN,
        SUPERADMIN
    }
    public class UserProfile : BaseDapperModel
    {
        public int Id { get; set; }

        public string? ExternalId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public UserRole Role { get; set; } = UserRole.USER;
        public string? PhoneNumber { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsArchived { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}