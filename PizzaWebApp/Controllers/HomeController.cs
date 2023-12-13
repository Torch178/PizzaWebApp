//Video Walkthrough:
//https://www.loom.com/share/2f6cbc12b38849fdaf4da4e6afbf2487

using Microsoft.AspNetCore.Mvc;
using PizzaWebApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Dynamic;

namespace PizzaWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UpgradePizzaForm(Pizza pizza) 
        {
            ViewBag.pizza = pizza;
            return View();
        }

        [HttpPost]
        public IActionResult UpgradePizzaForm(Pizza pizza, string submitButton)
        {
            var Size = pizza.Size;
            var ExtraCheese = pizza.ExtraCheese;
            switch (submitButton)
            {
                //User Confirms Pizza Options, Add to Order
                case "confirm":
                    
                    if (Size == "Large") { pizza.Price += 4; }
                    else if (Size == "ExtraLarge") { pizza.Price += 6; }
                    if (ExtraCheese == true) { pizza.Price += 1.50M; }

                    Repository.AddPizza(pizza);
                    return View("SuccessPage");

                //User Cancels Action, pizza is discarded and user returned to menu
                case "cancel":
                    return View("Index");

                default:
                    return View("Index");
            }
        }

        public IActionResult SuccessPage() 
        { 
            return View(); 
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                
                Repository.Order = order;
                return View("OrderSummary");
            }
            else
            {
                return View();
            }
                

        }

        [HttpGet]
        public IActionResult OrderSummary()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult OrderSummary(int id)
        {
            Repository.RemovePizza(id);
            if(Repository.Pizzas == null)
            {
                return View("Index");
            }
            else
            {
                Repository.Order.CalculateCost();
                return View("OrderSummary");
            }
            
        }

        public IActionResult ThankYou()
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


