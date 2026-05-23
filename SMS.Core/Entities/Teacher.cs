using System.ComponentModel.DataAnnotations;

namespace SMS.Domain.Entities
{
    public class Teacher
    { 
        public enum enTeacherStatus : byte
        {
            Active = 1,          // مستمر
            vacation = 2,        // مجاز 
            Transferred = 3,     // منقول
            Retired = 4          // متقاعد
        }

        [Required(ErrorMessage = "Teacher ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Teacher ID is invalid.")]
        public int TeacherID { get; set; } // هذا الحقل هو نفسه PersonID

        [Required(ErrorMessage = "Qualification is required.")]
        [StringLength(50, ErrorMessage = "Qualification cannot exceed 50 characters.")]
        public string Qualification { get; set; } = null!;

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExitDate { get; set; }

        [Required(ErrorMessage = "Teacher status is required.")]
        public enTeacherStatus TeacherStatus { get; set; }

        [Required(ErrorMessage = "Created By User ID is required.")]
        [Range(1,int.MaxValue, ErrorMessage = "Created By User ID is invalid.")]
        public int CreatedByUserID { get; set; }

        public virtual User CreatedByUser { get; set; } = null!;

        public virtual Person Person { get; set; } = null!;
    }
}
