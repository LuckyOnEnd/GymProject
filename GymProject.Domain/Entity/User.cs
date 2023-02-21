using GymProject.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsPaid { get; set; }
        public bool Confirmmail { get; set; }
        public DateTime DatePaid { get; set; }
        public Role Role { get; set; }
        public Profile Profile { get; set; }
    }
}
