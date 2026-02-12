using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.DTOs
{
    public class CreateTaskDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        public bool IsCompleted { get; set; }   
    }
}
