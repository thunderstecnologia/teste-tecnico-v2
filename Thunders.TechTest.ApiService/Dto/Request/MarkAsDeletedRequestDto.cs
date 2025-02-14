using System.ComponentModel.DataAnnotations;

namespace Thunders.TechTest.ApiService.Dto.Request
{
    public class MarkAsDeletedRequestDto
    {
        [Required]
        public long Id { get; set; }

        [Required, MaxLength(100)]
        public required string DeletedBy { get; set; }
    }
}
