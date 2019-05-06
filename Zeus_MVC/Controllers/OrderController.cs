using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Zeus_MVC.Models;
using Zeus_MVC.ViewModels;

namespace Zeus_MVC.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        ApplicationDbContext context;

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public OrderController()
        {
            context = new ApplicationDbContext();
        }

        public OrderController(ApplicationUserManager userManager) : base()
        {
            UserManager = userManager;
        }
        

        // GET: Order
        public async Task<ActionResult> Index()
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            var userOrders = context.Orders.Where(o => o.UserId == user.Id).ToList();

            if (userOrders == null)
                return RedirectToAction("Error404", "Home");

            return View(userOrders);
        }

        // GET: Order/Success
        public ActionResult Success()
        {
            return View();
        }

        // GET: Order/Success
        public async Task<ActionResult> Details(int id)
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            var order = context.Orders.SingleOrDefault(o => o.Id == id);

            var orderdetails = context.OrderDetails.Include(p => p.Product).Where(o => o.OrderId == id).ToList();


            var viewModel = new OrderDetailViewModel()
            {
                Client = user,
                Order = order,
                OrderDetails = orderdetails
            };

            return View(viewModel);
        }
    }
}