using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Domain.ViewModels.Account
{
    public class EmailViewModels
    {
        [Required(ErrorMessage = "Wpisz email")]
        [DataType(DataType.Password)]
        [Display(Name = "Value")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wpisz wiadomość")]
        [DataType(DataType.Password)]
        [Display(Name = "Value")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Wpisz Imie lub Nazwisko")]
        [DataType(DataType.Password)]
        [Display(Name = "Value")]
        public string Name { get; set; }

        public string Numer { get; set; }
    }
}
