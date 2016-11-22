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
        public GridViewDataSet<Cart> Carts { get; set; } = new GridViewDataSet<Cart>
        {
            SortExpression = nameof(Cart.CartID),
            PageSize= 20,
            SortDescending = false
        };

        public override Task PreRender()
        {
            CartService.LoadDataCart(Carts);
            return base.PreRender();
        }
    }
}

