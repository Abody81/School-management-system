namespace SMS.Domain.Entities;

public partial class Enrollment
{
    public enum enStatus { Active = 1, vacation = 2, Promoted = 3, Failed = 4, Dropped = 5, Transferred = 6 }

    public int EnrollmentID { get; set; }

    public int StudentID { get; set; }

    public int ClassID { get; set; }

    public string AcademicYear { get; set; } = null!;

    public enStatus Status { get; set; }

    public string? Notes { get; set; }

    public int CreatedByUserID { get; set; }

    public DateOnly? EnrollmentDate { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;

    public virtual ICollection<StudentGrade> StudentGrades { get; set; } = new List<StudentGrade>();
}
