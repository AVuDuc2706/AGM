using System.ComponentModel.DataAnnotations;

namespace AccountService.Models.DTOs
{
    public class AccountDto
    {
        public Guid AccountId { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string DisplayName { get; set; } = string.Empty;

        public Guid ApplicationTypeId { get; set; }

        public string? AppTypeName { get; set; }

        public Guid UserId { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
