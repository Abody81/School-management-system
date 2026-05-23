using SMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SMS.Domain.Entities
{
    public class GradeLevel
    {
        [Key]
        [Required(ErrorMessage = "Grade level ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Grade level ID must be a positive integer.")]
        public int GradeLevelID { get; set; } // IDENTITY(1,1)


        [Required(ErrorMessage = "Grade level name is required.")]
        [StringLength(10, ErrorMessage = "Grade level name cannot exceed 10 characters.")]
        public string GradeLevelName { get; set; } = null!; // Renamed to avoid collision with class name

        
        [Required(ErrorMessage = "Stage ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid stage.")]
        public int StageID { get; set; } 


        [Required(ErrorMessage = "Track ID is required.")] 
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid track.")]
        public int TrackID { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<ClassSubject> ClassSubjects { get; set; } = new List<ClassSubject>();
        
        public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

        public virtual Stage Stage { get; set; } = null!;

        public virtual Track Track { get; set; } = null!;
    }
}




