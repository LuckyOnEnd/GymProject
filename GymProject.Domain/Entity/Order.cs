using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Domain.Entity
{
    public class Order
    {
        public int Id { get; set; }

        public long UserId { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
