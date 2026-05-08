using SMS.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace SMS.Core.Entities
{
    public class Guardian
    {

        [Required(ErrorMessage = "Guardian ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a person")]
        public int GuardianID { get; set; }

        [StringLength(50, ErrorMessage = "Job Title cannot exceed 50 characters.")]
        public string ?JobTitle { get; set; }

        [Required(ErrorMessage = "Created By User ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Created By User ID must be a positive integer.")]
        public int CreatedByUserID { get; set; }

        public virtual User CreatedByUser { get; set; } = null!;

        public virtual Person Person { get; set; } = null!;

        public virtual ICollection<Guardians_Student> Guardians_Students { get; set; } = new List<Guardians_Student>();
    }
}
