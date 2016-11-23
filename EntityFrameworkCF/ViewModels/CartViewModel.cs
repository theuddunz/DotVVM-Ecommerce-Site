using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Runtime.Filters;
using System.Threading.Tasks;

namespace EntityFrameworkCF.ViewModels
{
    [Authorize]
    public class CartViewModel : MasterpageViewModel
    {
        public double total { get; set; }
        public string Message { get; set; } = "Your Cart Is Empty.";
        public bool Enabled { get; set; } = false;

        public GridViewDataSet<CartItem> CartItems { get; set; } = new GridViewDataSet<CartItem>
        {
            SortExpression = nameof(CartItem.CartItemID),
            PageSize = 5,
            SortDescending = false
        };

        public void Remove(int itemid)
        {
            using (var db = new Database())
            {
                var cartitem = db.CartItems.Find(itemid);
                var cart = db.Carts.Find(cartitem.CartID);
                cart.Count--;
                db.CartItems.Remove(cartitem);
                db.SaveChanges();
                CartService.LoadDataCart(CartItems);
                total = Convert.ToDouble(CartService.GetTotal());
            }
        }

        public override Task PreRender()
        { 
            if (CartService.GetTotal() == 0)
            {
                Enabled = true;
            }
            total = Convert.ToDouble(CartService.GetTotal());
            CartService.LoadDataCart(CartItems);
            return base.PreRender();

        }
    }
}

