using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeus_MVC.Models;

namespace Zeus_MVC.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _context;

        public ProductController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }


        [ChildActionOnly]
        public ActionResult ProductSection()
        {
            try
            {
                var ProductsList = _context.Products.OrderByDescending(p => p.Id).Take(8).ToList();

                return PartialView(ProductsList);
            }
            catch
            {
                return PartialView();
            }
        }
    }
}