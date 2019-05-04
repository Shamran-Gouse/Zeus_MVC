using PagedList;
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
        public ActionResult Index(int? page)
        {
            var ProductsInDb = _context.Products.OrderByDescending(p => p.Id).ToList();

            if (ProductsInDb == null)
                return HttpNotFound();

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(ProductsInDb.ToPagedList(pageNumber, pageSize));
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

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var ProductsInDb = _context.Products.SingleOrDefault(p => p.Id == id);

            if (ProductsInDb == null)
                return HttpNotFound();

            return View(ProductsInDb);
        }
    }
}