using GymProject.Domain.Entity;
using GymProject.Domain.Helpers;
using GymProject.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe.Infrastructure;
using Stripe;

namespace GymProject.Controllers
{
    public class PaymentController : Controller
    {
        private IMembershipService _service;
        public PaymentController(IMembershipService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Pay(decimal price)
        {
            return View(price);
        }

        public IActionResult PaypalPartial()
        {
            return PartialView();
        }
        public IActionResult Charge(string stripeEmail, string stripeToken, decimal stripePrice)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();


            var customer = customers.Create(new CustomerCreateOptions { 
                Email= stripeEmail, 
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions { 
                Amount = (long)stripePrice,
                Description="Test Payment",
                Currency="usd",
                Customer=customer.Id,
                ReceiptEmail = stripeEmail,
                Metadata=new Dictionary<string, string>()
                {
                    {"OrderId", "111" },
                    { "Postcode", "LEE111"},
                }
            });

            if(charge.Status == "succeeded")
            {
                string BalanceTransactionId = charge.BalanceTransactionId;
                return View();
            }
            else
            {

            }
            return View();
        }
    }   
}
