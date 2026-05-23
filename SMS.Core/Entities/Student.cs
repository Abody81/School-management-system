using System.ComponentModel.DataAnnotations;

namespace SMS.Domain.Entities
{
    public class Student
    {
        public enum enStudentStatus : byte
        {
            Active = 1,
            Vacation = 2,
            Graduated = 3,
            Transferred = 4,
            DroppedOut = 5
        }

        [Required(ErrorMessage = "Student ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Student ID.")]
        public int StudentID { get; set; }


        [Required(ErrorMessage = "Class selection is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Class ID.")]
        public int CurrentClassID { get; set; }


        [Required(ErrorMessage = "Enrollment date is required.")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }


        [Required(ErrorMessage = "Student status is required.")]
        public enStudentStatus StudentStatus { get; set; }


        [DataType(DataType.Date)]
        public DateTime? GraduationDate { get; set; }


        [Required(ErrorMessage = "Creator User ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid User ID.")]
        public int CreatedByUserID { get; set; }

        public virtual Person Person { get; set; } = null!;

        public virtual User CreatedByUser { get; set; } = null!;

        public virtual Class CurrentClass { get; set; } = null!;

        public virtual ICollection<Guardians_Student> Guardians_Students { get; set; } = new List<Guardians_Student>();

        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    }
}

