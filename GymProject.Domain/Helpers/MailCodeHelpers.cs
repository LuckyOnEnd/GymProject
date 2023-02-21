using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Domain.Helpers
{
    public static class MailCodeHelpers
    {
        public static string MailCode()
        {
            Random rnd = new Random();
            int random = rnd.Next();
            return random.ToString();
        }
    }
}
