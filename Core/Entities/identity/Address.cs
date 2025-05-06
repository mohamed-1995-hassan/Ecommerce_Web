
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Core.Entities.identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public void SetAddressValues(string firstName, string lastName, string city, string zip,
                                     string state, string street)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Zip = zip;
            State = state;
            Street = street;
        }
    }
}
