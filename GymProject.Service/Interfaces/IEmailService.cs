using GymProject.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Service.Interfaces
{
    public interface IEmailService
    {
        bool ConfirmEmail(string text, string mail);

        bool ContactEmailSend(string text, string mail, string name);
    }
}
