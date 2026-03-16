using System.ComponentModel.DataAnnotations;

namespace AccountService.Models
{
    public class ApplicationType
    {
        [Key]
        public Guid ApplicationId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public ICollection<Account> Accounts { get; set; } = new List<Account>();

        public Guid UserId { get; set; }
    }
}
