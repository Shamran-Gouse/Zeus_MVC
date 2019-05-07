using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeus_MVC.Models;

namespace Zeus_MVC.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext _context;

        public ProductController()
        {
            _context = new ApplicationDbContext();
        }


        // GET: Admin/Product
        public ActionResult Index()
        {
            var ProductsInDb = _context.Products.ToList();

            if (ProductsInDb == null)
                return HttpNotFound();

            return View(ProductsInDb);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase productimage)
        {
            try
            {
                if (productimage == null)
                {
                    ModelState.AddModelError(string.Empty, "An image file must be chosen.");
                    return View("Create", product);
                }
                else if (ModelState.IsValid)
                {

                    var fileName = Path.GetFileName(productimage.FileName);
                    var path = Path.Combine(Server.MapPath("~/Product_images"), fileName);
                    productimage.SaveAs(path);


                    product.ImageUrl = fileName;
                    product.DateAdded = DateTime.Now;

                    _context.Products.Add(product);
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Product");

                }
                else
                {
                    return View("Create", product);
                }
            }
            catch
            {
                return View();
            }
        }


        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int id)
        {
            var ProductInDb = _context.Products.SingleOrDefault(p => p.Id == id);

            if (ProductInDb == null)
                return HttpNotFound();

            return View(ProductInDb);
        }

        // POST: Admin/Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product, HttpPostedFileBase productimage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var productInDb = _context.Products.Single(p => p.Id == id);

                    if (productimage != null)
                    {
                        // Delete the Existing Image
                        var filePath = Server.MapPath("~/Product_images/" + productInDb.ImageUrl);

                        System.IO.File.Delete(filePath);

                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        // ADD new Image
                        var fileName = Path.GetFileName(productimage.FileName);
                        var path = Path.Combine(Server.MapPath("~/Product_images"), fileName);
                        productimage.SaveAs(path);

                        productInDb.ImageUrl = fileName;

                    }

                    productInDb.PrductName = product.PrductName;
                    productInDb.Quantity = product.Quantity;
                    productInDb.ReOrder = product.ReOrder;
                    productInDb.Price = product.Price;
                    productInDb.Description = product.Description;

                    _context.SaveChanges();

                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    return View("Edit", product);
                }
            }
            catch
            {
                return View();
            }
        }


        // POST: Product/Delete/5
        //[HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var ProductInDb = _context.Products.SingleOrDefault(p => p.Id == id);

                _context.Products.Remove(ProductInDb);
                _context.SaveChanges();

                var filePath = Server.MapPath("~/Product_images/" + ProductInDb.ImageUrl);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                return RedirectToAction("Index", "Product");
            }
            catch
            {
                return View();
            }
        }


    }
}