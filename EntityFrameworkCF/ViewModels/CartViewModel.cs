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
        public GridViewDataSet<CartItem> CartItems { get; set; } = new GridViewDataSet<CartItem>
        {
            SortExpression = nameof(CartItem.CartItemID),
            PageSize= 20,
            SortDescending = false
        };

        public override Task PreRender()
        {
            CartService.LoadDataCart(CartItems);
            return base.PreRender();
        }
    }
}

