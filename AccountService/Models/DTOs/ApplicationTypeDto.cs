using System.ComponentModel.DataAnnotations;

namespace AccountService.Models.DTOs
{
    public class ApplicationTypeDto
    {
        public Guid ApplicationId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
