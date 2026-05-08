using System.ComponentModel.DataAnnotations;

namespace SMS.Core.Entities
{
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "PersonID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid person.")]
        public int PersonID { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(maximumLength: 20, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 20 characters.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password hash is required.")]
        [StringLength(maximumLength: 64 , MinimumLength = 64, ErrorMessage = "Password hash must be 64 characters.")]
        public string PasswordHash { get; set; } = null!;

        [Required(ErrorMessage = "Password salt is required.")]
        [StringLength(32, MinimumLength = 32, ErrorMessage = "Password salt must be 32 characters.")]
        public string PasswordSalt { get; set; } = null!;

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Permissions salt is required.")]
        public short? Permissions { get; set; }

        [Required(ErrorMessage = "Created By User ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Created By User ID is invalid.")]
        public int CreatedByUserID { get; set; }

        public virtual User CreatedByUser { get; set; } = null!;

        public virtual ICollection<Guardian> Guardians { get; set; } = new List<Guardian>();

        public virtual ICollection<User> InverseCreatedByUser { get; set; } = new List<User>();

        public virtual Person Person { get; set; } = null!;

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();

        public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();

    }
}

