using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeus_MVC.Areas.Admin.Models;
using Zeus_MVC.Models;

namespace Zeus_MVC.Areas.Admin.Controllers
{
    // add LoginPage property in AdminAuthorization
    //[AdminAuthorization(LoginPage = "~/Account/LoginAdmin")]
    [AdminAuthorization(Roles = "Admin")]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}