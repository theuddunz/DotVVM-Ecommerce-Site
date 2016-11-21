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
            var identity = HttpContext.Current.GetOwinContext().Authentication.User.Identity as ClaimsIdentity;
            if (identity.IsAuthenticated)
            {
                using (var db = new Database())
                {
                    var id = UserService.GetCurrentUserId();
                    var cartquery = from p in db.Carts
                                    where p.UserID == id
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
            var identity = HttpContext.Current.GetOwinContext().Authentication.User.Identity as ClaimsIdentity;
            if (identity.IsAuthenticated)
            {
                using (var db = new Database())
                {
                    var userid = UserService.GetCurrentUserId();
                    var querycart = from p in db.Carts
                                    where p.UserID == userid
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

        public static void LoadDataCart(GridViewDataSet<Cart> dataset)
        {
            var userid = UserService.GetCurrentUserId();
            using (var db = new Database())
            {
                var load = from p in db.Carts
                           where p.UserID == userid
                           select p;
                dataset.LoadFromQueryable(load);
            }
        }
    }
}