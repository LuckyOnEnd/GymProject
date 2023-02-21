using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Domain.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Wpisz imię")]
        [MaxLength(20, ErrorMessage = "Imię musi być mniej niż 20 symbolow")]
        [MinLength(3, ErrorMessage = "Imię musi być dluższej niż 3 sybmoly")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Wpisz hasło")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
    }
}
