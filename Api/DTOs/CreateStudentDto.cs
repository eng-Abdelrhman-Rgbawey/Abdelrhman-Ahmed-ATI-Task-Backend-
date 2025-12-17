using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class CreateStudentDto
    {
        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^01[0-2,5]{1}[0-9]{8}$",
            ErrorMessage = "Invalid Egyptian phone number")]
        public string Phone { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(2)]
        public List<int> CourseIds { get; set; }
    }
}
