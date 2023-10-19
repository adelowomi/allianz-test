using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Allianz.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string DateOfBirth { get; set; }
    }
}