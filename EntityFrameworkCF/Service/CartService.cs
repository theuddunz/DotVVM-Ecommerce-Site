using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityFrameworkCF.ViewModels
{
    public class CartService
    {
        public static int? GetCartID()
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
    }
}