using GymProject.DAL.Interfaces;
using GymProject.Domain.Entity;
using GymProject.Domain.Enum;
using GymProject.Domain.Response;
using GymProject.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Service.Implementations
{
    public class EmailService : IEmailService
    {

        private readonly IBaseRepository<Profile> _proFileRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly ILogger<AccountUserService> _logger;
        public EmailService(IBaseRepository<User> userRepository,
            ILogger<AccountUserService> logger, IBaseRepository<Profile> proFileRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            _proFileRepository = proFileRepository;
        }
        public bool ConfirmEmail(string text, string mail)
        {
            //var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => (x.Id == id));
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                message.From = new MailAddress("testdevelopdotnet@outlook.com", "The Power");
                message.To.Add(mail);
                message.Subject = "Podtwerdzenie poczty";
                message.IsBodyHtml = true;
                message.Body = text;

                smtpClient.Port = 587;
                smtpClient.Host = "smtp.office365.com";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("testdevelopdotnet@outlook.com", "admin123456");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(message);

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetBaseException().Message);

                return false;
            }
        }

        public bool ContactEmailSend(string text, string mail, string name)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                message.From = new MailAddress("testdevelopdotnet@outlook.com", "The Power");
                message.To.Add("trusviacheslav@gmail.com");
                message.Subject = name;
                message.IsBodyHtml = true;
                message.Body = "Mail: " + mail + "\n" + text;

                smtpClient.Port = 587;
                smtpClient.Host = "smtp.office365.com";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("testdevelopdotnet@outlook.com", "admin123456");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(message);

                return true;
            }
            catch
            {
                return false;
            } 
            
        }
    }
}
