using DotVVM.Framework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace EntityFrameworkCF.ViewModels
{
    public class CartService
    {       
        public static int? GetCartID()
        {
            if (UserService.GetCurrentUserId() != null)
            {
                using (var db = new Database())
                {
                    var user = db.Users.Find(UserService.GetCurrentUserId());
                    var cartquery = from p in db.Carts
                                    where p.UserID == user.UserID
                                    select p;
                    var cartid = cartquery.FirstOrDefault();
                    return cartid.CartID;
                }
            }
            else
            {

                return null;
            }


        }
        public static int? GetCartCountItem()
        {
            if (UserService.GetCurrentUserId() != null)
            {
                using (var db = new Database())
                {
                    var userid = db.Users.Find(UserService.GetCurrentUserId());

                    var querycart = from p in db.Carts
                                    where p.UserID == userid.UserID
                                    select p;

                    if (querycart.Count() != 0)
                    {
                        var cartid = db.Carts.Find(GetCartID());
                        return cartid.Count;
                    }
                    else
                    {
                        var cart = new Cart();
                        cart.UserID = Convert.ToInt32(UserService.GetCurrentUserId());
                        return cart.Count;
                    }

                }
            }
            else
            {
                return 0;
            }


        }

        public static void LoadDataCart(GridViewDataSet<CartItem> dataset)
        {
            using (var db = new Database())
            {
                var user = db.Users.Find(Convert.ToInt32(UserService.GetCurrentUserId()));
                var load = from p in db.CartItems
                           where p.CartID == user.CartID
                           select p;
                dataset.LoadFromQueryable(load);

            }
        }
        public static double? GetTotal()
        {
            using (var db = new Database())
            {
                var user = db.Users.Find(Convert.ToInt32(UserService.GetCurrentUserId()));
                var load = from p in db.CartItems
                           where (p.CartID == user.CartID)
                           select p.Price;
                var total = load.DefaultIfEmpty(0).Sum();               
                return total;
            }
        }
    }
}