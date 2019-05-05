using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Zeus_MVC.Models
{
    public partial class ShoppingCart
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();
        string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Product product, int quantity)
        {
            // Get the matching cart and product instances
            var cartItem = storeDB.CartItmes.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ProductId == product.Id);

            var QuantityInStock = product.Quantity;

            if (cartItem == null)
            {
                if (QuantityInStock < quantity)
                {
                    quantity = QuantityInStock;
                }



                // Create a new cart item if no cart item exists
                cartItem = new CartItem
                {
                    ProductId = product.Id,
                    CartId = ShoppingCartId,
                    Quantity = quantity,
                    DateCreated = DateTime.Now
                };
                storeDB.CartItmes.Add(cartItem);
            }
            else
            {

                // If the item does exist in the cart, 
                // caculate the order quantity
                int OrderQuantity = cartItem.Quantity + quantity;

                // if OrderQuantity is less than or equal to QuantityInStock its ok
                if (QuantityInStock >= OrderQuantity)
                {
                    cartItem.Quantity = OrderQuantity;
                }
                else
                {
                    // else put QuantityInStock
                    cartItem.Quantity = QuantityInStock;
                }
            }
            // Save changes
            storeDB.SaveChanges();
        }

        public void ChangeQuantity(Product product, int quantity)
        {
            // Get the matching cart and product instances
            var cartItem = storeDB.CartItmes.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ProductId == product.Id);

            var QuantityInStock = product.Quantity;

            if (cartItem != null)
            {
                if (QuantityInStock < quantity)
                {
                    quantity = QuantityInStock;
                }

                cartItem.Quantity = quantity;

                // Save changes
                storeDB.SaveChanges();
            }
        }

        public void RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = storeDB.CartItmes.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id);

            if (cartItem != null)
            {
                storeDB.CartItmes.Remove(cartItem);

                // Save changes
                storeDB.SaveChanges();
            }
        }

        public void EmptyCart()
        {
            var cartItems = storeDB.CartItmes.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                storeDB.CartItmes.Remove(cartItem);
            }
            // Save changes
            storeDB.SaveChanges();
        }

        public List<CartItem> GetCartItems()
        {
            return storeDB.CartItmes
                .Where(cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in storeDB.CartItmes
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Quantity).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in storeDB.CartItmes
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Quantity *
                              cartItems.Product.Price).Sum();

            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    OrderId = order.Id,
                    UnitPrice = item.Product.Price,
                    Quantity = item.Quantity
                };

                // Set the order total of the shopping cart
                orderTotal += (item.Quantity * item.Product.Price);

                storeDB.OrderDetails.Add(orderDetail);

            }

            // Set the order's total to the orderTotal count
            order.TotalBill = orderTotal;

            // Save the order
            storeDB.SaveChanges();

            // Empty the shopping cart
            EmptyCart();

            // Return the OrderId as the confirmation number
            return order.Id;
        }


        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name.ToString();
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();

                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }


        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var CartItmsToBeChanged = storeDB.CartItmes.Where(c => c.CartId == ShoppingCartId).ToList();

            var AlreadyExisItems = storeDB.CartItmes.Where(c => c.CartId == userName).ToList();

            

            foreach (CartItem item in CartItmsToBeChanged)
            {
                var userItem = AlreadyExisItems.SingleOrDefault(cartItem => cartItem.ProductId == item.ProductId);

                if (userItem != null)
                {
                    // update the Quantity
                    userItem.Quantity = userItem.Quantity + item.Quantity;

                    // Remove Duplicate
                    storeDB.CartItmes.Remove(item);
                }
                else
                {
                    item.CartId = userName;
                }

            }

            storeDB.SaveChanges();



            //foreach (CartItem item in CartItmsToBeChanged)
            //{

            //    if (AlreadyExisItems.Any(c => c.ProductId == item.ProductId))
            //    {

            //        var userItem = storeDB.CartItmes.SingleOrDefault(cart => cart.RecordId == item.RecordId);

            //        if (userItem != null)
            //        {
            //            // update the Quantity
            //            userItem.Quantity = userItem.Quantity + item.Quantity;
            //            storeDB.SaveChanges();

            //            // Remove Duplicate
            //            storeDB.CartItmes.Remove(item);
            //            storeDB.SaveChanges();
            //        }
            //    }
            //    else
            //    {
            //        item.CartId = userName;
            //        storeDB.SaveChanges();
            //    }

            //}

            //storeDB.SaveChanges();
        }
    }
}