namespace SMS.Domain.Entities;

public class Person
{
    public int PersonID { get; set; }
    public string FirstName { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public string ThirdName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public char Gender
    {
        get { return field; }

        set { field = char.ToUpper(value); }
    }

    public DateTime DateOfBirth { get; set; }
    public string NationalNumber { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public int CountryID { get; set; }
    public string Address { get; set; } = null!;
    public string ImagePath { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual Guardian? Guardian { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Teacher? Teacher { get; set; }

    public virtual User? User { get; set; }

    public void Update(string firstName, string secondName, string thirdName, string lastName,
        char gender, DateTime dateOfBirth, string nationalNumber, string? phoneNumber,
        string address, int countryId, string imagePath)
    {
        FirstName = firstName;
        SecondName = secondName;
        ThirdName = thirdName;
        LastName = lastName;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        NationalNumber = nationalNumber;
        PhoneNumber = phoneNumber;
        Address = address;
        CountryID = countryId;
    }

    public static Person Create(string firstName, string secondName, string thirdName, string lastName,
    char gender, DateTime dateOfBirth, string nationalNumber, string? phoneNumber,
    string address, int countryID, string imagePath)
    {
        return new Person
        {
            FirstName = firstName,
            SecondName = secondName,
            ThirdName = thirdName,
            LastName = lastName,
            Gender = gender,
            DateOfBirth = dateOfBirth,
            NationalNumber = nationalNumber,
            PhoneNumber = phoneNumber,
            Address = address,
            CountryID = countryID,
            ImagePath = imagePath
        };
    }

    public void SetImage(string ImagePath)
    {
        if (string.IsNullOrWhiteSpace(ImagePath))
            throw new ArgumentException("Image path cannot be empty.");

        this.ImagePath = ImagePath;
    }
}
