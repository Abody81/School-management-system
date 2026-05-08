using System.ComponentModel.DataAnnotations;

namespace SMS.Core.Entities
{
    public class Country
    {
        [Required(ErrorMessage = "Country ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid country.")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Country name is required.")]
        [StringLength(80, ErrorMessage = "Country name cannot exceed 80 characters.")]
        public string CountryName { get; set; } = null!;

        public virtual ICollection<Person> People { get; set; } = new List<Person>();

    }
}