using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeus_MVC.Areas.Admin.Models;
using Zeus_MVC.Models;

namespace Zeus_MVC.Areas.Admin.Controllers
{
    [AdminAuthorization(Roles = "Admin")]
    public class SearchController : Controller
    {
        ApplicationDbContext _context;

        public SearchController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Admin/Search/Product
        public ActionResult Product()
        {
            return View();
        }


        // POST: Admin/Search/Product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Product(string query)
        {
            var ProductsInDb = _context.Products
                                        .Where(p => p.PrductName.Contains(query) 
                                        || p.Description.Contains(query)).ToList();

            if (ProductsInDb == null)
                TempData["error"] = "NO Data on record.";

            return View(ProductsInDb);
        }
    }
}