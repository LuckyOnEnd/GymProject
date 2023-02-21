using GymProject.Domain.Entity;
using GymProject.Domain.Helpers;
using GymProject.Domain.Response;
using GymProject.Domain.ViewModels.Cart;
using GymProject.Models;
using GymProject.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GymProject.Controllers
{
    public class CartController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;
        private readonly IMembershipService membershipService;
        private IHttpContextAccessor httpContextAccessor;
        IConfiguration _configuration;
        private static string Value { get; set; }
        private string Name { get; set; }

        public CartController(IUserService userService, ILogger<AccountController> logger, IEmailService emailService, IConfiguration configuration, IHttpContextAccessor context, IMembershipService mebershipService)
        {
            _userService = userService;
            _logger = logger;
            membershipService = mebershipService;
            _configuration = configuration;
            httpContextAccessor = context;
        }

        [HttpGet]
        public async Task<IActionResult> Pay(int memberID)
        {
            var respone = await membershipService.GetMembership(memberID);
            Name = respone.Data.Name.ToString(); 
            Value = respone.Data.Price.ToString().Replace(',','.');
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult PaymentWithPaypal(string Cancel = null, string blogId = "", string PayerId = "", string guid = "")
        {
            var ClientId = _configuration.GetValue<string>("PayPal:Key");
            var ClientSecret = _configuration.GetValue<string>("PayPal:Secret");
            var mode = _configuration.GetValue<string>("PayPal:mode");
            APIContext apiContext = PaypalConfiguration.GetAPIContext(ClientId, ClientSecret, mode);

            try
            {
                string payerId = PayerId;
                if (string.IsNullOrEmpty(payerId))
                {
                    string baseURI = this.Request.Scheme + "://" + this.Request.Host + "/Home/PaymentWithPayPal?";

                    var guidd = Convert.ToString((new Random()).Next(100000));
                    guid = guidd;

                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, blogId);

                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    { 
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    httpContextAccessor.HttpContext.Session.SetString("payment", createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var paymentId = httpContextAccessor.HttpContext.Session.GetString("payment");
                    var executedPayment = ExecutePayment(apiContext, payerId, paymentId as string);

                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return RedirectToAction("Membership", "GetMemberships");
                    }

                    var blogIds = executedPayment.transactions[0].item_list.items[0].sku;
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl, string blogId)
        {
            var itemList = new ItemList()
            {
                items = new List<Item>()
            }; itemList.items.Add(new Item()
            {
                name = Name,
                currency = "PLN",
                price = Value,
                quantity = "1",
                sku = "asd"
            });

            var payer = new Payer()
            {
                payment_method = "paypal"
            };

            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            var amount = new Amount()
            {
                currency = "PLN",
                total = Value
            };
            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = Guid.NewGuid().ToString(),
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return this.payment.Create(apiContext);
        }
    }

}
