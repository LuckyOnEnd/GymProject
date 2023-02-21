using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Domain.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Wpisz imię")]
        [MaxLength(20, ErrorMessage = "Imie musi być mniej niz 20 symblów")]
        [MinLength(3, ErrorMessage = "Imie musi bic dluzszej niz 3 symbola")]

        public string FirstName { get; set; }

        [Required(ErrorMessage = "Wpisz nazwisko")]
        [MaxLength(20, ErrorMessage = "Nazwisko musi być mniej niz 20 symblów")]
        [MinLength(3, ErrorMessage = "Nazwisko musi bic dluzszej niz 3 symbola")]

        public string Surname { get; set; }

        [Required(ErrorMessage = "Wpisz username")]
        [MaxLength(20, ErrorMessage = "Username musi być mniej niz 20 symblów")]
        [MinLength(3, ErrorMessage = "Username musi bic dluzszej niz 3 symbola")]

        public string Name { get; set; }

        [Required(ErrorMessage = "Wpisz mail")]

        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Wpisz haslo")]
        [MinLength(6, ErrorMessage = "Haslo musi byc dluzszej niz 6 symbolow")]

        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Podtwierdz haslo")]
        [Compare("Password", ErrorMessage = "Haslo nie zgadza sie")]

        public string PasswordConfirm { get; set; }

    }
}
