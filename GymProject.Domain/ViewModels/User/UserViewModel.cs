using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Domain.ViewModels.User
{
    public class UserViewModel
    {
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Wpisz role")]
        [Display(Name = "Role")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Wpisz login")]
        [Display(Name = "Login")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Haslo")]
        [Display(Name = "Haslo")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Surname")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Imie")]
        [Display(Name = "Imie")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Imie")]
        [Display(Name = "Imie")]
        public bool IsPaid { get; set; }
        public bool Confirmmail { get; set; }
        public DateTime? DatePaid { get; set; }
    }
}
