using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zeus_MVC.Models;

namespace Zeus_MVC.Areas.Admin.Models
{
    public class AppCount
    {
        static ApplicationDbContext _context = _context = new ApplicationDbContext();

        public static int GetUnpaidOrderCount()
        {
            var count = _context.Orders.Where(o => o.Status == OrderStatus.UnPaid).Count();

            if (count <= 0)
                return 0;

            return count;
        }

        public static int GetUnReadMessageCount()
        {
            var count = _context.ContactMessages.Where(c => c.Status == MessageStatus.UnRead).Count();

            if (count <= 0)
                return 0;

            return count;
        }

        public static int GetProductsCount()
        {
            var count = _context.Products.Count();

            if (count <= 0)
                return 0;

            return count;
        }

        public static int GetProductsReOrderCount()
        {
            var count = _context.Products.Where(p => p.Quantity <= p.ReOrder).Count();

            if (count <= 0)
                return 0;

            return count;
        }

        public static int GetClientCount()
        {
            var count = _context.Users.Where(u => u.ClientName != "").Count();

            if (count <= 0)
                return 0;

            return count;
        }
    }
}