using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp_Bugeto.Models;
using WebApp_Bugeto.Services;

namespace WebApp_Bugeto.Controllers
{
    public class HomeController : Controller
    {

        private readonly IProductService _productService;
        private readonly IMessage _message;
        public HomeController(IProductService productService, IMessage message)
        {
            _productService = productService;
            _message = message;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProduct(Product product)
        {
            _productService.AddProduct(product);
            _productService.ProductCount += 1;
            return View();
        }


        public IActionResult SendMessage(string Message,int UserId, Messagetype messagetype)
        {
            switch (messagetype)
            {
                case Messagetype.Sms:
                    _message.Sms(Message, UserId);
                    break;
                case Messagetype.Email:
                     _message.Email(Message, UserId);

                    break;
                case Messagetype.Notif:
                    _message.Notif(Message, UserId);
                    break;
               
            }


            return Json(true);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


