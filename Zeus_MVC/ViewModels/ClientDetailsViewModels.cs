using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zeus_MVC.Models;

namespace Zeus_MVC.ViewModels
{
    public class ClientDetailsViewModels
    {
        public ApplicationUser Client { get; set; }

        public List<Order> Order { get; set; }
    }
}