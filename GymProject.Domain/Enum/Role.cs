using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Domain.Enum
{
    public enum Role
    {
        [Display(Name = "User")]
        User = 0,
        [Display(Name = "Moderator")]
        Moderator = 1,
        [Display(Name = "Admin")]
        Admin = 2,
    }
}
