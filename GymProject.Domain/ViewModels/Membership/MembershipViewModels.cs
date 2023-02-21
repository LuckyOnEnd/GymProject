using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Domain.ViewModels.Membership
{
    public class MembershipViewModels
    {
        public string Name { get; set; }

        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; } // duration in one day
        public IFormFile Avatar { get; set; }

        public byte[]? Image { get; set; }
    }
}
