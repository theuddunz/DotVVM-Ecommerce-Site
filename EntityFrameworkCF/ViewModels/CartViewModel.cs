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
        public double total { get; set; } = CartService.GetTotal();
        public GridViewDataSet<CartItem> CartItems { get; set; } = new GridViewDataSet<CartItem>
        {
            SortExpression = nameof(CartItem.CartItemID),
            PageSize= 20,
            SortDescending = false
        };

        public void Remove(int itemid)
        {
            using (var db = new Database())
            {
                db.CartItems.Remove(itemid);
                db.SaveChanges();
                CartService.LoadDataCart(CartItems);
            }
        }

        public override Task PreRender()
        {
            CartService.LoadDataCart(CartItems);
            return base.PreRender();
        }
    }
}

