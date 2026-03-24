using System.ComponentModel.DataAnnotations;

namespace AccountService.Models
{
    public class Account
    {
        [Key]
        public Guid AccountId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string DisplayName { get; set; } = string.Empty;

        public Guid ApplicationTypeId { get; set; }
        public ApplicationType ApplicationType { get; set; }

        public Guid UserId { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
