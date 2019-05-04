using System.Collections.Generic;
using Zeus_MVC.Models;

namespace Zeus_MVC.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}