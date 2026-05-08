using SMS.Core.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SMS.Core.Entities
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(20, ErrorMessage = "First name cannot exceed 20 characters.")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "FirstName can only contain letters and spaces.")]
        public string FirstName { get; set; } = null!;
        
        [Required(ErrorMessage = "Second name is required.")]
        [StringLength(20, ErrorMessage = "Second name cannot exceed 20 characters.")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "SecondName can only contain letters and spaces.")]
        public string SecondName { get; set; } = null!;

        [Required(ErrorMessage = "Third name is required.")]
        [StringLength(20, ErrorMessage = "Third name cannot exceed 20 characters.")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "ThirdName can only contain letters and spaces.")]
        public string ThirdName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(20, ErrorMessage = "Last name cannot exceed 20 characters.")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "LastName can only contain letters and spaces.")]
        public string LastName { get; set; } = null!;


        private char _gender;
        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression("^[MF]$", ErrorMessage = "Gender must be 'M' for Male or 'F' for Female.")]
        public char Gender 
        {
            get { return _gender ; }

            set { _gender = char.ToUpper(value); }
        }


        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        [MinAge(5)]
        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "National number is required.")]
        [StringLength(15, ErrorMessage = "National number cannot exceed 15 characters.")]
        public string NationalNumber { get; set; } = null!;


        [StringLength(12, MinimumLength = 8, ErrorMessage = "Phone number must be between 8 and 12 characters.")]
        public string? PhoneNumber { get; set; } 


        [Required(ErrorMessage = "Country ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid country.")]
        public int CountryID { get; set; }


        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string Address { get; set; } = null!;


        [Required(ErrorMessage = "Image is required.")]
        [StringLength(350, ErrorMessage = "Image path cannot exceed 350 characters.")]
        public string ImagePath { get; set; } = null!;

        public virtual Country Country { get; set; } = null!;

        public virtual Guardian? Guardian { get; set; }

        public virtual Student? Student { get; set; }

        public virtual Teacher? Teacher { get; set; }

        public virtual User? User { get; set; }
    }
}