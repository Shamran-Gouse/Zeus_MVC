using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zeus_MVC.Models
{
    public class AdminAuthorization : AuthorizeAttribute
    {
        // to pass login URL from controller
        //public string LoginPage { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated || filterContext.HttpContext.User.IsInRole("User"))
            {
                filterContext.HttpContext.Response.Redirect("~/Account/AdminLogin");
            }
            base.OnAuthorization(filterContext);
        }
    }
}