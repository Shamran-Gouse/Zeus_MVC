using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zeus_MVC.Models;

namespace Zeus_MVC.ViewModels
{
    public class OrderDetailViewModel
    {
        public Order Order { get; set; }

        public ApplicationUser Client { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}