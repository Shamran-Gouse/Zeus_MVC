using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zeus_MVC.Areas.Admin.Models
{
    public class AdminAuthorization : AuthorizeAttribute
    {
        // to pass login URL from controller
        //public string LoginPage { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated || !filterContext.HttpContext.User.IsInRole("Admin"))
            {
                filterContext.HttpContext.Response.Redirect("~/Account/AdminLogin");
            }
            base.OnAuthorization(filterContext);
        }
    }
}