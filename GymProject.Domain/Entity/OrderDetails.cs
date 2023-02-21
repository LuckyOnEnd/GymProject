using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Domain.Entity
{
    public class OrderDetails
    {   
        public int Id { get; set; }

        public long UserId { get; set; }

        public long OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quanity { get; set; }

        public DateTime CreatedAt { get; set; }
        public OrderDetails Orders { get; set; }

    }
}
