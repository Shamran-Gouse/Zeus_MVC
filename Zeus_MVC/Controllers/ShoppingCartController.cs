using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeus_MVC.Models;
using Zeus_MVC.ViewModels;

namespace Zeus_MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext storeDB;
        public ShoppingCartController()
        {
            storeDB = new ApplicationDbContext();
        }

        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            // Return the view
            return View(viewModel);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult AddToCart(int id)
        {
            var ProductInDb = storeDB.Products.SingleOrDefault(p => p.Id == id);

            var cartItem = new CartItem
            {
                Product = ProductInDb

            };

            return PartialView("AddToCart", cartItem);
        }

        // POST: /ShoppingCart/AddToCart/id?5/quantity?20
        [HttpPost]
        public ActionResult AddToCart(int id, CartItem cartItem)
        {
            // Retrieve the Product from the database
            var ProductInDb = storeDB.Products.Single(product => product.Id == id);

            if (cartItem.Quantity < 1 || string.IsNullOrEmpty(cartItem.Quantity.ToString()))
            {
                cartItem.Quantity = 1;
            }

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(ProductInDb, cartItem.Quantity);

            return RedirectToAction("Index");
        }

        //
        // POST: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Remove from cart
            cart.RemoveFromCart(id);

            return RedirectToAction("Index");
        }


        //
        // POST: /ShoppingCart/ChangeQuantity/5
        [HttpPost]
        public ActionResult ChangeQuantity(int id, int quantity)
        {
            // Retrieve the Product from the database
            var ProductInDb = storeDB.Products.Single(product => product.Id == id);

            if (quantity < 1 || string.IsNullOrEmpty(quantity.ToString()))
            {
                quantity = 1;
            }

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.ChangeQuantity(ProductInDb, quantity);

            return RedirectToAction("Index");
        }

        //
        // GET: /ShoppingCart/EmptyCart
        [HttpGet]
        public ActionResult EmptyCart()
        {
            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.EmptyCart();

            return RedirectToAction("Index");
        }



        //
        // GET: /ShoppingCart/CheckOut
        [Authorize]
        public ActionResult CheckOut()
        {
            // Add it to the shopping cart
            //var cart = ShoppingCart.GetCart(this.HttpContext);

            //cart.EmptyCart();

            return RedirectToAction("Index");
        }

    }
}