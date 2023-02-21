using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Domain.ViewModels.Account
{
    public class EmailConfirm
    {
        [Required(ErrorMessage = "Wpisz numer")]
        [DataType(DataType.Password)]
        [Display(Name = "Value")]
        public string Value { get; set; }
    }
}
