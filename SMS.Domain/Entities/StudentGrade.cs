#nullable enable

using SMS.Domain.Entities;

namespace  SMS.Domain.Entities;

public partial class StudentGrade
{
    public int GradeID { get; set; }

    public int EnrollmentID { get; set; }

    public int SubjectID { get; set; }

    public byte ExamTypeID { get; set; }

    public decimal Mark { get; set; }

    public string? Notes { get; set; }

    public virtual Enrollment Enrollment { get; set; } = null!;

    public virtual ExamType ExamType { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
