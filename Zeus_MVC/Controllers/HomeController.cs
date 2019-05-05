using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeus_MVC.Models;

namespace Zeus_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/Error404
        public ActionResult Error404()
        {
            return View();
        }

        // add LoginPage property in AdminAuthorization
        //[AdminAuthorization(LoginPage = "~/Account/LoginAdmin")]
        [AdminAuthorization]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // POST: Home/Contact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactMessage contactMessage)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    contactMessage.Date = DateTime.Now;
                    contactMessage.Status = MessageStatus.UnRead;

                    _context.ContactMessages.Add(contactMessage);
                    _context.SaveChanges();

                    return RedirectToAction("ThanksForContact", "Home");

                }
                else
                {
                    return View("Index", contactMessage);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/ThanksForContact
        public ActionResult ThanksForContact()
        {
            return View();
        }

        // GET: Home/Legaldisclaimer
        public ActionResult Legaldisclaimer()
        {
            return View();
        }

        // GET: Home/PrivacyPolicy
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}