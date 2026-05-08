using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SMS.DTOs.PersonDTOs
{
    public class PersonFillterDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid person ID")]
        public int? PersonID { get; set; }


        [StringLength(20, ErrorMessage = "First name cannot exceed 20 characters.")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "FirstName can only contain letters and spaces.")]
        public string? FirstName { get; set; }

        [StringLength(20, ErrorMessage = "Second name cannot exceed 20 characters.")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "SecondName can only contain letters and spaces.")]
        public string? SecondName { get; set; }

        [StringLength(20, ErrorMessage = "Third name cannot exceed 20 characters.")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "ThirdName can only contain letters and spaces.")]
        public string? ThirdName { get; set; }

        [StringLength(20, ErrorMessage = "Last name cannot exceed 20 characters.")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "LastName can only contain letters and spaces.")]
        public string? LastName { get; set; }

        
        [StringLength(15, ErrorMessage = "National number cannot exceed 15 characters.")]
        public string? NationalNumber { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Invalid PageNumber")]
        public int PageNumber { get; set; } = 1;


        [Range(1, int.MaxValue, ErrorMessage = "Invalid PageNumber")]
        public  int RowsPerPage { get; set; } = 10;
    }
}
