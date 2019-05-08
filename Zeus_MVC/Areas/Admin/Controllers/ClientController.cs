using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeus_MVC.Areas.Admin.Models;
using Zeus_MVC.Models;
using Zeus_MVC.ViewModels;

namespace Zeus_MVC.Areas.Admin.Controllers
{
    [AdminAuthorization(Roles = "Admin")]
    public class ClientController : Controller
    {
        ApplicationDbContext _context;

        public ClientController()
        {
            _context = new ApplicationDbContext();
        }


        // GET: Admin/Client
        public ActionResult Index()
        {
            var ClientInDb = _context.Users.Where(u => u.ClientName != null).ToList();

            if (ClientInDb == null)
                return HttpNotFound();

            return View(ClientInDb);
        }

        // GET: Admin/Client/Details/5
        public ActionResult Details(string id)
        {
            var ClientInDb = _context.Users.SingleOrDefault(u => u.Id == id);

            var order = _context.Orders.Where(o => o.UserId == id).ToList();

            if (ClientInDb == null)
                return HttpNotFound();

            var viewModel = new ClientDetailsViewModels()
            {
                Client = ClientInDb,
                Order = order,
            };

            return View(viewModel);
        }
    }
}