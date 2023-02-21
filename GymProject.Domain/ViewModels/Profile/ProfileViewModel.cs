using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Domain.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "age")]
        [Range(0, 150, ErrorMessage = "Диапазон возраста должен быть от 0 до 150")]
        public byte Age { get; set; }

        [Required(ErrorMessage = "WPISZ ADRES")]
        [MinLength(5, ErrorMessage = ">5")]
        [MaxLength(200, ErrorMessage = "<200")]
        public string Address { get; set; }

        public string UserName { get; set; }

        public string NewPassword { get; set; }
    }
}
