using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Zeus_MVC.Models;
using Zeus_MVC.ViewModels;
using System.Data.Entity;

namespace Zeus_MVC.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        ApplicationDbContext _context;

        public OrderController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Admin/Order
        public ActionResult Index()
        {
            try
            {
                var Orders = _context.Orders.Include(o => o.ApplicationUser).OrderByDescending(o => o.Id).ToList();

                return View(Orders);
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int id)
        {
            var order = _context.Orders.SingleOrDefault(o => o.Id == id);

            var user = _context.Users.SingleOrDefault(u => u.Id == order.UserId);

            var orderdetails = _context.OrderDetails.Include(p => p.Product).Where(o => o.OrderId == id).ToList();


            var viewModel = new OrderDetailViewModel()
            {
                Client = user,
                Order = order,
                OrderDetails = orderdetails
            };

            return View(viewModel);
        }


        // POST: Admin/Order/MarkPaid/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MarkPaid(int id)
        {
            try
            {
                var Order = _context.Orders.SingleOrDefault(o => o.Id == id);

                if (Order == null)
                    return HttpNotFound();


                Order.Status = OrderStatus.Paid;
                _context.SaveChanges();

                return RedirectToAction("Details", "Order", new { id = id });
            }
            catch
            {
                return RedirectToAction("Index", "Order");
            }
        }
    }
}
