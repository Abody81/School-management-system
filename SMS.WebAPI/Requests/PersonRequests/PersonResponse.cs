#pragma warning disable

using SMS.Domain.Attributes;
using SMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SMS.WebAPI.DTOs.PersonDTOs
{
    public class PersonResponse
    {
        public int PersonID { get; set; }

        public string FirstName { get; set; } = null!;

        public string SecondName { get; set; } = null!;

        public string ThirdName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        private char _gender;
        public char Gender
        {
            get { return _gender; }

            set { _gender = char.ToUpper(value); }
        }

        public DateTime DateOfBirth { get; set; }

        public string NationalNumber { get; set; }

        public string PhoneNumber { get; set; }

        public int CountryID { get; set; }

        public string Address { get; set; }

        public string ImageURl { get; set; }

        public string CountryName { get; set; }
    }
}
